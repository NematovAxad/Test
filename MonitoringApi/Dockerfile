FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 6053

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
RUN dotnet nuget add source http://172.16.60.9:2000/v3/index.json -n LocalBaget
WORKDIR /src
COPY . .
COPY ["MonitoringApi/MonitoringApi.csproj", "MonitoringApi/"]
RUN dotnet restore "MonitoringApi/MonitoringApi.csproj"
#COPY . .
WORKDIR "/src/MonitoringApi"
RUN dotnet build "MonitoringApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonitoringApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonitoringApi.dll"]
