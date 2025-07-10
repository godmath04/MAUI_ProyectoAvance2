using RubiaDivinaWebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RubiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RubiaDBContext")));

// Configurar CORS para React Native
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactNativePolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configuración para mejorar compatibilidad con React Native
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener PascalCase
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
DbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactNativePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
