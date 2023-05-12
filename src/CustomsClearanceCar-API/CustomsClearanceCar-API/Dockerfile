#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomsClearanceCar-API/CustomsClearanceCar-API.csproj", "CustomsClearanceCar-API/"]
RUN dotnet restore "CustomsClearanceCar-API/CustomsClearanceCar-API.csproj"
COPY . .
WORKDIR "/src/CustomsClearanceCar-API"
RUN dotnet build "CustomsClearanceCar-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomsClearanceCar-API.csproj" -c Release -o /app/c3/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/c3/publish .
ENTRYPOINT ["dotnet", "CustomsClearanceCar-API.dll"]