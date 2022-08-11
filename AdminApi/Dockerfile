FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 6051

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
RUN dotnet nuget add source http://172.16.60.9:2000/v3/index.json -n LocalBaget
WORKDIR /src
COPY ["AdminApi/AdminApi.csproj", "AdminApi/"]
RUN dotnet restore "AdminApi/AdminApi.csproj"
COPY . .
WORKDIR "/src/AdminApi"
RUN dotnet build "AdminApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminApi.dll"]
