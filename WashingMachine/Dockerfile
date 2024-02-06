FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 36000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WashingMachine/WashingMachine.csproj", "WashingMachine/"]
RUN dotnet restore "WashingMachine/WashingMachine.csproj"
COPY . .
WORKDIR "/src/WashingMachine"
RUN dotnet build "WashingMachine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WashingMachine.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WashingMachine.dll"]