using JStudio.Api.Data;
using JStudio.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); // Byggaren skapas

// Add services to the container. // Öppnar containern
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Kontakt med SqlServer och aktuell databas med hjälp av connectionString

builder.Services.AddControllers().AddJsonOptions(jsonOptions => 
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAdService, AdService>();

var app = builder.Build(); // Stänger containern, ingredienserna till bygget av appen


// "Appandet nedan" styr hur varje anrop hanteras och ordningen här är vital



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(swaggerOptions =>
    {
        swaggerOptions.SwaggerEndpoint("/openapi/v1.json", "JStudio api");
        swaggerOptions.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
