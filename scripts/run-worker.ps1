Write-Host "Starting [ElderlyCare.Workers.RobotHandler]..."
./load-dotenv.ps1

dotnet run --project ..\src\Workers.RobotHandler\ElderlyCare.Workers.RobotHandler.csproj --urls=$env:RobotWorkerUrl