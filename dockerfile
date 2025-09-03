# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY WeatherAPI.csproj ./
RUN dotnet restore WeatherAPI.csproj
COPY . .
RUN dotnet publish WeatherAPI.csproj -c Release -o /app/out /p:UseAppHost=false

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet","WeatherAPI.dll"]