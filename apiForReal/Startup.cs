using System;
using System.Text;
using apiForReal.admin.api;
using apiForReal.models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;


namespace apiForReal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {

                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)

                };

            });

            services.AddSingleton(jwtTokenConfig);
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddRouting();
            services.AddMvc();
            services.AddControllers();
            services.AddOData();
            services.AddDbContext<DbTicketsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicketDatabase")));
        }
            

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapODataRoute("odata", null, GetEdmModel());
                endpoints.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }

        private static IEdmModel GetEdmModel()
        {

            var builder = new ODataConventionModelBuilder();

            builder.EnableLowerCamelCase();
            builder.EntitySet<Customer>("Customer");
            builder.EntitySet<Worker>("Worker");
            builder.EntitySet<Department>("Department");
            builder.EntitySet<User>("User");
            builder.EntitySet<Ticket>("Ticket");
            /*EntityTypeConfiguration<Ticket> tickets = builder.EntitySet<Ticket>("Ticket").EntityType;

            tickets.HasKey(t => t.CustomerId);
            tickets.HasKey(t => t.DepartmentId);
            tickets.HasKey(t => t.WorkerId);

            tickets.HasRequired(t => t.CustomerId, (o, c) => o.CustomerId == c.Id);*/

            return builder.GetEdmModel();
        }



    }
}
