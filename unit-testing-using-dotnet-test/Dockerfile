# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY webapi-dockerized/*.csproj ./webapi-dockerized/
RUN dotnet restore

# copy everything else and build app
COPY webapi-dockerized/. ./webapi-dockerized/
WORKDIR /source/webapi-dockerized
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "webapi-dockerized.dll"]