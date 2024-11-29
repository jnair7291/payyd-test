using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

configuration
 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
 .AddEnvironmentVariables();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Payyd API",
        Version = "v1",
        Description = "API documentation for Payyd"
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(options => options.PreSerializeFilters.Add((swagger, httpReq) =>
{
    if (httpReq.Host.Host == "agw-app-nonprod-eastus.abem.org")
    {
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "/" + builder.Environment.EnvironmentName + "/payment" } };
    }
}));

app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payyd");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
