FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY ./src/EJ2APIServices.csproj ./src/
RUN dotnet restore ./src/EJ2APIServices.csproj

COPY ./ ./src/
WORKDIR /app/src
RUN dotnet publish ./EJ2APIServices.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5001

ENTRYPOINT ["dotnet", "EJ2APIServices_NET.dll"]
