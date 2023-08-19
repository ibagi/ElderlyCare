FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ./Api /src/Api
COPY ./Data /src/Data
COPY ./Domain /src/Domain

RUN dotnet restore Api/ElderlyCare.Api.csproj

WORKDIR /src/Api/
RUN dotnet publish -c Release -o build -r linux-x64 --self-contained

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0
COPY --from=build /src/Api/build ./app
WORKDIR /app

ENTRYPOINT ["./ElderlyCare.Api"]