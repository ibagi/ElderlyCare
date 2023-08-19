FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /src

COPY ./Worker /src/Worker
COPY ./Data /src/Data
COPY ./Domain /src/Domain

RUN dotnet restore Worker/ElderlyCare.Worker.csproj

WORKDIR /src/Worker/
RUN dotnet publish -c Release -o build -r linux-x64 --self-contained

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0
COPY --from=build /src/Worker/build ./app
WORKDIR /app

ENTRYPOINT ["./ElderlyCare.Worker"]