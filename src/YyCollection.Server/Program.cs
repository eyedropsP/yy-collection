using YyCollection.Server.Internals.OpenApi;
using YyCollection.Server.Internals.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .ConfigureAppConfiguration(static (context, builder) => { builder.AddAppConfiguration(context); })
    .ConfigureServices(static (context, services) =>
    {
        var appSettings = services.ConfigureHerokuAppSettings(context.Configuration);
        services.AddPerformance();
        services.AddRequestRouting();
        services.AddAspNetCoreMvc();
        services.AddDomainServices(appSettings);
        
        if (context.HostingEnvironment.IsDevelopment())
            services.AddOpenApi();
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseResponseCompression();
app.UseWebSockets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(static endpoints =>
{
    endpoints.MapControllers();
});

app.Run();