
//--- Configure application

using YyCollection.Server.Internals.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .ConfigureAppConfiguration(static (context, builder) => { builder.AddHerokuAppConfiguration(context); })
    .ConfigureServices(static (context, services) =>
    {
        var appSettings = services.ConfigureAppSettings(context.Configuration);
        services.AddDomainServices(appSettings);
    });

// Add services to the container.

builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

app.Run();