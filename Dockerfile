FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-step
WORKDIR /src
COPY . .
RUN dotnet restore "./WebApi/WebApi.csproj" --disable-parallel
RUN dotnet publish "./WebApi/WebApi.csproj" -c Release -o /Deployment --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS serve-step
WORKDIR /src
COPY --from=build-step /Deployment ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ASPNETCORE_URLS=http://+:$PORT dotnet WebApi.dll