using MediatR;
using System.Reflection;
using TaxPayerTaxCalculation.Application.Handlers;
using TaxPayerTaxCalculation.Application.Repositories;
using TaxPayerTaxCalculation.Application.Services;
using TaxPayerTaxCalculation.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITaxPayerService, TaxPayerService>();
builder.Services.AddTransient<ITaxPayerRepository, TaxPayerRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(typeof(CalculateTaxesCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.Configure<CalculationSettings>(
    builder.Configuration.GetSection("Calculations"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
