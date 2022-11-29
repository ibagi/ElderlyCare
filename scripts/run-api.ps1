Write-Host "Starting [ElderlyCare.Api]..."
./load-dotenv.ps1

dotnet run --project ..\src\Api\ElderlyCare.Api.csproj --urls=$env:ApiUrl