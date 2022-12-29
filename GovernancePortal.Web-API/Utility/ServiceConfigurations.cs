using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FluentValidation;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Core.Utilities;
using GovernancePortal.EF;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Validators.Meeting;
using GovernancePortal.Service.Validators.Resolution;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace GovernancePortal.Web_API.Utility;

public static class ServiceConfigurations
{
    
    public static void ConfigureGovernancePortalServices(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddTransient<IValidator<Meeting>, MeetingValidator>();
        services.AddTransient<IValidator<VotingUser>, VotingUserValidator>();
        services.AddCors(options => options.AddPolicy("*",
                  builder =>
                  {
                      builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                             .WithOrigins("*")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .Build();
                  }));
        services.AddHttpContextAccessor();
        

        services.AddDbContext<PortalContext>(opt =>
               opt.UseSqlServer(EnvironmentVariables.ConnectionString,
                  x => x.MigrationsAssembly("CorporateUniverse.EF"))
                  .EnableSensitiveDataLogging());

        /*services.AddDbContext<PortalContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DbConnection"),
                    x => x.MigrationsAssembly("GovernancePortal.EF"))
                .EnableSensitiveDataLogging());*/

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BodAdmin API Governance Portal", Version = "v1" });
        });
        services.ConfigureServiceForAuthentication_Authorization(Configuration);

    }

    public static void ConfigureServiceForAuthentication_Authorization(this IServiceCollection services, IConfiguration Configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = EnvironmentVariables.BODADMINIssuer,
                ValidAudience = EnvironmentVariables.BODADMINAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.BODADMINSecretKey))
            };

        }).AddCookie("cookie");

        services.AddHttpContextAccessor();

        services.AddAuthorization();
    }

    public static void UseGovernancePortalServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BodAdmin_Api_Governance_Portal v1"));
        app.UseAuthentication();
        app.UseAuthorization();

    }
}