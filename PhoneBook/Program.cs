using Contacts.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Contacts.Security;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();           //dodaj repozytoria
builder.Services.AddSecServices(builder.Configuration); //dodaj konfiguracjê JWT z warstw¹ autentykacji

builder.Services.AddAuthorization();

builder.Services.AddCors(           //ka¿da aplikacja z tej samej domeny mo¿e odczytaæ API
    opt =>
    {
        opt.AddPolicy("Open",
            builder => builder.AllowAnyOrigin()
            .AllowAnyHeader().AllowAnyMethod());
    });

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); //zezwól na fetchowanie 
                                                                                                  //danych zale¿nych

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ContactsDatabaseContext>                                  //rejestracja DB
    (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Open");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
