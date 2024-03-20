using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OnlinerParser;
using OnlinerParser.Repositories.Abstractions;
using OnlinerParser.Repositories.Implementations;
using OnlinerParser.Services.Abstractions;
using OnlinerParser.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("CommonFactory", _ => { })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (_, _, _, _) => true
    });

builder.Services.AddDbContext<OnlinerParserContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.

builder.Services.AddScoped<IOnlinerParserService, OnlinerParserService>();

builder.Services.AddScoped<IOnlinerParserRepository, OnlinerParserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();