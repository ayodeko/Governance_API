FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:6.0-buster AS build
WORKDIR /
COPY . .

RUN dotnet restore "GovernancePortal.Web-API/GovernancePortal.Web-API.csproj"
WORKDIR "/."
COPY . .

RUN dotnet build "GovernancePortal.Web-API/GovernancePortal.Web-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GovernancePortal.Web-API/GovernancePortal.Web-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GovernancePortal.Web-API.dll"]
