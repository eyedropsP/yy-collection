namespace YyCollection.Client;

public class StartUp
{
    public IConfiguration Configuration { get; }

    public StartUp(IConfiguration configuration)
        => this.Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddAuthentication()
            .AddTwitter(o =>
            {
                o.ConsumerKey = this.Configuration["Authentication_Twitter_ConsumerApiKey"];
                o.ConsumerSecret = this.Configuration["Authentication_Twitter_ConsumerApiSecrets"];
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(static endpoints =>
            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Todos}/{action=Index}/{id?}"));
    }
}