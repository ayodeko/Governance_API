using GovernancePortal.Web_API.Endpoints;
using GovernancePortal.Web_API.Utility;



var builder = WebApplication.CreateBuilder(args);

builder.RegisterMeetingServices();
builder.Services.ConfigureGovernancePortalServices(builder.Configuration);




var app = builder.Build();

app.MapSectionedMeetingEndpoints();
app.UseGovernancePortalServices();
app.UseCors("*");
app.UseGovernancePortalExceptionHandler();
app.Run();