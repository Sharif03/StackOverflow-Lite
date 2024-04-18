using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using StackOverflowLite.Application;
using StackOverflowLite.Domain;
using StackOverflowLite.Infrastructure;
using StackOverflowLite.Infrastructure.Extensions;
using StackOverflowLite.Web;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using StackOverflowLite.Infrastructure.Email;
using Amazon.SQS;

var builder = WebApplication.CreateBuilder(args);

//Serilog setup
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    //Autofac Setup
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new DomainModule());
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new WebModule());

        // Register AWS SQS client
        containerBuilder.Register(c => new AmazonSQSClient()).As<IAmazonSQS>();
    });

    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddIdentity();
    builder.Services.AddControllersWithViews();
    builder.Services.AddCookieAuthentication();
    builder.Services.AddHttpContextAccessor();

    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    Log.Information("Application Starting...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Failed to start application.");
}
finally
{
    Log.CloseAndFlush();
}
