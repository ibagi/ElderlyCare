using ElderlyCare.Domain.Configuration;
using ElderlyCare.Migrations.SchemaMigrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

static string env(string key) => Environment.GetEnvironmentVariable(key)!;

var connectionString = new ConnectionString(
    env("DbServer"),
    env("DbDatabase"),
    env("DbUser"),
    env("DbPassword"));

// Configure the dependency injection services
var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(connectionString.ToString())
        .ScanIn(typeof(AddRobotsTable).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

// Update the database
var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();
