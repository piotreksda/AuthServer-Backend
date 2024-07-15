FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../OAuth2OpenId.Application/OAuth2OpenId.Application.csproj", "../OAuth2OpenId.Application/"]
RUN dotnet restore "../OAuth2OpenId.Application/OAuth2OpenId.Application.csproj"
COPY . .
WORKDIR "/src/../OAuth2OpenId.Application"
RUN dotnet build "OAuth2OpenId.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OAuth2OpenId.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAuth2OpenId.Application.dll"]
