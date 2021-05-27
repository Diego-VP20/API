using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using static TokenController;

public interface IJwtAuthManager
{

    JwtAuthResult GenerateToken(string username, Claim[] claims, DateTime now, string url);

}

public class JwtAuthManager : IJwtAuthManager
{

    public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();
    private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;
    private readonly JwtTokenConfig _jwtTokenConfig;
    private readonly byte[] _secret;

    private static string GenerateRefreshTokenString()
    {

        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);

    }

    public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
    {
        _jwtTokenConfig = jwtTokenConfig;
        _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
        _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
    }

    public JwtAuthResult GenerateToken(string username, Claim[] claims, DateTime now, string url)
    {
        var shouldAddAudienceCLaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

        var jwtToken = new JwtSecurityToken(
            _jwtTokenConfig.Issuer,
            shouldAddAudienceCLaim ? _jwtTokenConfig.Audience : string.Empty,
            claims,
            expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret),
            SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var refreshToken = new RefreshToken
        {

            UserName = username,
            TokenString = GenerateRefreshTokenString(),
            ExpiresAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration),
            Url = url + "/refresh"

        };

        _usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

        return new JwtAuthResult
        {

            AccessToken = accessToken,
            TokenType = "bearer",
            IssuedAt = now,
            ExpireAt = now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
            RefreshToken = refreshToken

        };

    }

    public class RefreshToken
    {

        [JsonPropertyName("token_string")]
        public string TokenString { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("expires")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

    }

}