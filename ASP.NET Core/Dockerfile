FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ./src/EJ2APIServices.csproj ./src/NuGet.Config ./
RUN dotnet restore EJ2APIServices.csproj

COPY ./src/ ./
RUN dotnet publish EJ2APIServices.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Startup.cs creates App_Data/Documents at boot; the K8s pod runs as uid/gid 1000,
# which cannot mkdir under the root-owned /app. Pre-create it and hand it over.
RUN mkdir -p /app/App_Data/Documents && chown -R 1000:1000 /app/App_Data
USER 1000

EXPOSE 5001

ENTRYPOINT ["dotnet", "EJ2APIServices.dll"]
