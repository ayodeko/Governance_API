using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using GovernancePortal.EF.Repository;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Implementation;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using GovernancePortal.Service.Mappings.Maps;
using GovernancePortal.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.WebAPI
{
    public class Startup
    {
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

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

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskMaps, TaskMaps>();
            services.AddScoped<IExceptionHandler, ExceptionHandler>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITaskRepo, TaskRepo>();
            services.AddScoped<IMeetingService_depr, MeetingServiceDeprDepr>();
            services.AddScoped<IMeetingsRepo_depr, MeetingRepoDeprDepr>();
            services.AddScoped<IMeetingMaps_depr, MeetingMaps_depr>();

            //services.AddHttpContextAccessor();

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

            //Inject all Services
            //services.AddIdentityCore<IdentityUser>()
            //    .AddEntityFrameworkStores<PortalContext>()
            //    .AddDefaultTokenProviders();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BodAdmin API Governance Portal", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BodAdmin_Api_Goverance_Portal v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(allowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGovernancePortalExceptionHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
