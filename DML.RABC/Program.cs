using Serilog;
using Serilog.Events;
namespace DML.RABC;

public class Program
{
    public static void Main(string[] args)
    {
        //TestSeriLog(args);
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        

        var app = builder.Build();
        //Add Seri
        //app.UseSerilogRequestLogging(options =>
        //{
        //    // Customize the message template
        //    options.MessageTemplate = "Handled {RequestPath}";

        //    // Emit debug-level events instead of the defaults
        //    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        //    // Attach additional properties to the request completion event
        //    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        //    {
        //        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        //        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        //    };
        //});
        //var logger = new LoggerConfiguration()
        //                .ReadFrom.Configuration(builder.Configuration)
        //                .Enrich.FromLogContext()
        //                .CreateLogger();
        //builder.Host.UseSerilog(logger);
        //builder.Services.AddSerilog();
        //builder.Services.AddSerilog((services, lc) => lc
        //.ReadFrom.Configuration(builder.Configuration)
        //.ReadFrom.Services(services)
        //.Enrich.FromLogContext()
        //.WriteTo.Console());

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
        
    }
    public static void TestSeriLog(string[] args)
    {
        

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSerilog(); // <-- Add this line

            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
