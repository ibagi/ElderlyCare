Write-Host "Migrating up the local database..."
./load-dotenv.ps1

dotnet build ..\src\Migrations\ElderlyCare.Migrations.csproj -c Release
dotnet ..\src\Migrations\bin\Release\net6.0\ElderlyCare.Migrations.dll