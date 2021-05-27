using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using static JwtAuthManager;

[ApiController]
public class TokenController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public TokenController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("token")]

    public JwtAuthResult Token([FromForm] TokenRequest request)
    {

        var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();

        var now = DateTime.Now;
        var claims = new[]
        {

            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, "test")

        };

        var url = $"{Request.Scheme}://{Request.Host}";

        var token = jwtAuthManager.GenerateToken(request.UserName, claims, now, url);

        return token;

    }


    public class TokenRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class JwtAuthResult
    {

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("issued")]
        public DateTime IssuedAt { get; set; }

        [JsonPropertyName("expires")]
        public DateTime ExpireAt { get; set; }

        [JsonPropertyName("refresh_token")]
        public RefreshToken RefreshToken { get; set; }

    }

}