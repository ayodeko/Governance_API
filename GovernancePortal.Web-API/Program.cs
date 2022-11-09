using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using GovernancePortal.EF.Repository;
using GovernancePortal.Service.Implementation;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using GovernancePortal.Service.Mappings.Maps;
using GovernancePortal.Web_API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var conn = @"Server=(LocalDb)\MSSQLLocalDB;Initial Catalog=governancePortal;Integrated Security=True;";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PortalContext>(opt =>
    opt.UseSqlServer(conn,
            x => x.MigrationsAssembly("GovernancePortal.EF"))
        .EnableSensitiveDataLogging());

builder.RegisterCreateMeetingEndpointServices();

builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IMeetingsRepo, MeetingRepo>();
builder.Services.AddScoped<IMeetingMaps, MeetingMaps>();
builder.Services.AddScoped<ILogger, StubLogger>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BodAdmin API Governance Portal", Version = "v1" });
});


var app = builder.Build();

app.MapSectionedMeetingEndpoints();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BodAdmin_Api_Governance_Portal v1"));

app.Run();