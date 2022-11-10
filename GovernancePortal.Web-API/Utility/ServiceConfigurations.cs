using System.Text;
using GovernancePortal.EF;
using GovernancePortal.Service.ClientModels.General;
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
        string allowSpecificOrigins = "_allowSpecificOrigins";
        services.AddCors(options =>
        {
            options.AddPolicy(allowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        
        services.Configure<UploadConfig>(options =>
        {
            options.AccessKey = Configuration["ExternalProviders:AwsS3:AccessKey"];
            options.AccessSecret = Configuration["ExternalProviders:AwsS3:AccessSecret"];
            options.BucketName = Configuration["ExternalProviders:AwsS3:Bucket"];
        });
        
        services.AddDbContext<PortalContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DbConnection"),
                    x => x.MigrationsAssembly("GovernancePortal.EF"))
                .EnableSensitiveDataLogging());
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BodAdmin API Governance Portal", Version = "v1" });
        });

    }

    public static void ConfigureServiceForAuthentication_Authorization(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]))
                };
                options.Events = new JwtBearerEvents
                {
                    //add cookie based
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["BodAccess"];
                        return Task.CompletedTask;
                    },
                };
            });

        services.AddAuthorization();
    }

    public static void UseGovernancePortalServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BodAdmin_Api_Governance_Portal v1"));

    }
}