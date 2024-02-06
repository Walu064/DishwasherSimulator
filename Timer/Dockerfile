#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 36006

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Timer/Timer.csproj", "Timer/"]
RUN dotnet restore "Timer.csproj"
COPY . .
WORKDIR "/src/Timer"
RUN dotnet build "Timer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timer.dll"]