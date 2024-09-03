using SearchMaster.Application;
using SearchMaster.DataAccess;
using SearchMaster.Extensions;
using SearchMaster.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services
    .AddApiAuthentication(configuration)
    .AddRepository(configuration)
    .AddServices()
    .AddInfrastructure();


services.AddSwaggerGen();

services.Configure<EmailOptions>(configuration.GetSection(nameof(EmailOptions)));
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

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
