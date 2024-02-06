#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 36004

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WaterPump/WaterPump.csproj", "WaterPump/"]
RUN dotnet restore "WaterPump/WaterPump.csproj"
COPY . .
WORKDIR "/src/WaterPump"
RUN dotnet build "WaterPump.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WaterPump.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaterPump.dll"]