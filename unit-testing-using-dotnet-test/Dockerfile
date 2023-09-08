# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY PrimeService/*.csproj ./PrimeService/
COPY PrimeService.API/*.csproj ./PrimeService.API/
COPY PrimeService.Cli/*.csproj ./PrimeService.Cli/
COPY PrimeService.DAL/*.csproj ./PrimeService.DAL/
COPY PrimeService.Tests/*.csproj ./PrimeService.Tests/
RUN dotnet restore

# copy everything else and build app
COPY PrimeService/. ./PrimeService/
COPY PrimeService.API/. ./PrimeService.API/
COPY PrimeService.Cli/. ./PrimeService.Cli/
COPY PrimeService.DAL/. ./PrimeService.DAL/
COPY PrimeService.Tests/. ./PrimeService.Tests/
WORKDIR /source/PrimeService.API
RUN dotnet publish -c release -o /app --no-restore \
    --runtime alpine-x64 \
    --self-contained true \
    /p:PublishTrimmed=true


FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "PrimeService.API.dll"]