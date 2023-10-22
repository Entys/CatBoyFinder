FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CatBoyScrapper/CatBoyScrapper.csproj", "CatBoyScrapper/"]
RUN dotnet restore "CatBoyScrapper/CatBoyScrapper.csproj"
COPY . .
WORKDIR "/src/CatBoyScrapper"
RUN dotnet build "CatBoyScrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatBoyScrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatBoyScrapper.dll"]
