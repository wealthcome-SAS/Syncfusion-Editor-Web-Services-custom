FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./src/EJ2APIServices_NET8.csproj ./src/
RUN dotnet restore ./src/EJ2APIServices_NET8.csproj

COPY ./ ./src/
WORKDIR /app/src
RUN dotnet publish ./EJ2APIServices_NET8.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5001

ENTRYPOINT ["dotnet", "EJ2APIServices_NET8.dll"]
