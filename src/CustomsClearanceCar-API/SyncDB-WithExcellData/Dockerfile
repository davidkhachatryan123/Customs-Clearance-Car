#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SyncDB-WithExcellData/SyncDB-WithExcellData.csproj", "SyncDB-WithExcellData/"]
RUN dotnet restore "SyncDB-WithExcellData/SyncDB-WithExcellData.csproj"
COPY . .
WORKDIR "/src/SyncDB-WithExcellData"
RUN dotnet build "SyncDB-WithExcellData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SyncDB-WithExcellData.csproj" -c Release -o /app/syncdb/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/syncdb/publish .
ENTRYPOINT ["dotnet", "SyncDB-WithExcellData.dll"]