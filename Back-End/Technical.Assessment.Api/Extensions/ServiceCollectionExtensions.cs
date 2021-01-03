
#region Usings

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using System.Text;
using Technical.Assessment.Api.Filters;
using Technical.Assessment.Domain.Interfaces;
using Technical.Assessment.Infrastructure.Context;
using Technical.Assessment.Infrastructure.Implementations;

#endregion

namespace Technical.Assessment.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadExtensions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMvc();

            services.AddControllers(mvcOpts =>
            {
                mvcOpts.Filters.Add(typeof(AppExceptionFilterAttribute));
            });

            //Secret key which will be used later during validation
            string key = "my_secret_key_999444222888555_TEAM_INTERNATIONAL";

            var JwtSecretkey = Encoding.ASCII.GetBytes(key);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddOpenApiDocument(document =>
            {
                document.Title = "Technical assessment";
                document.Description = "Team International - Esteban Beckett";
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddEntityFrameworkSqlServer().AddDbContext<TechnicalAssessmentContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], sqlopts =>
                {
                    sqlopts.MigrationsHistoryTable("_MigrationHistory");
                });
            });


            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddCors();

            return services;
        }
    }
}
