using GovernancePortal.Service.Implementation;
using GovernancePortal.Web_API.Endpoints;
using GovernancePortal.Web_API.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterMeetingServices();
builder.Services.ConfigureGovernancePortalServices(builder.Configuration);

var app = builder.Build();
StaticLogics.Init(app.Services.GetRequiredService<IConfiguration>());
app.MapSectionedMeetingEndpoints();
app.MapResolutionEndpoints();
app.MapTaskMgtEndpoints();
app.UseGovernancePortalServices();
app.UseCors("*");
app.UseGovernancePortalExceptionHandler();
app.Run();