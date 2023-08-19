using ElderyCare.Data;
using MediatR;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddElderlyCareContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/assets",
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "assets"))
});

app.UseCors("EnableCors");

app.MapControllers();

app.Services.InitializeElderyCareContext();
app.Run();
