using Autofac.Extensions.DependencyInjection;
using Autofac;
using Serilog;
using Serilog.Events;
using StackOverflowLite.EmailService;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var migrationAssemblyName = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Application Starting up");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new WorkerModule());
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Start-up failed");
}
finally
{
    Log.CloseAndFlush();
}