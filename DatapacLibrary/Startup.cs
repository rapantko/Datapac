
using DatapacLibrary.Data;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using DatapacLibrary.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    /// <summary>
    /// This is where DI takes place
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers().AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDatabaseDeveloperPageExceptionFilter();

        //DB related
        services.AddDbContext<LibraryContext>(options =>
                options.UseLazyLoadingProxies(true).
                UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IArticleRepository<Book>, BookRepository>();
        services.AddScoped<IArticleRepository<User>, UserRepository>();
        services.AddScoped<IArticleRepository<Checkout>, CheckoutRepository>();
        services.AddScoped<IArticleRepository<Notification>, NotificationRepository>();

        //Business Related
        services.AddScoped<ILibraryService, DatapacLibraryService>();
        services.AddScoped<INotificationService<Email>, EmailService>();

        // Add Hangfire services.
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        // SwaggerGen annotations for correct regex pattern encoding
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });


    }
    /// <summary>
    /// Configure application before it starts running
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHangfireDashboard();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();
    }
}
