#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Workers.RobotHandler/ElderlyCare.Workers.RobotHandler.csproj", "Workers.RobotHandler/"]
COPY ["Data/ElderyCare.Data.csproj", "Data/"]
COPY ["Shared/ElderlyCare.Shared.csproj", "Shared/"]
RUN dotnet restore "Workers.RobotHandler/ElderlyCare.Workers.RobotHandler.csproj"
COPY . .
WORKDIR "/src/Workers.RobotHandler"
RUN dotnet build "ElderlyCare.Workers.RobotHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElderlyCare.Workers.RobotHandler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElderlyCare.Workers.RobotHandler.dll"]