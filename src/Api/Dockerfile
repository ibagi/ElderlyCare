FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /src

COPY ./Api /src/Api
COPY ./Data /src/Data
COPY ./Domain /src/Domain

RUN dotnet restore Api/ElderlyCare.Api.csproj

WORKDIR /src/Api/
RUN dotnet build -c Release

ENTRYPOINT ["dotnet", "run", "--no-build", "--no-launch-profile", "-c", "Release", "--"]