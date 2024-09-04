//using Serilog.AspNetCore;

namespace DML.RABC;

public class Program
{
    public static void Main(string[] args)
    {
        
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Add Seri
            //builder.Services.AddSerilog();
            //builder.Host.UseSerilog((context, configuration) =>
            //    configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseSerilogRequestLogging();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            throw new Exception("funny bone");
        }
        catch (Exception ex)
        {
            //Log.Fatal(ex, "Exception");
        }
        finally
        {
            //Log.CloseAndFlushAsync();
        }
    }
    //public static void TestSeriLog(string[] args)
    //{
        

    //    Log.Logger = new LoggerConfiguration()
    //        .WriteTo.Console()
    //        .CreateLogger();

    //    try
    //    {
    //        Log.Information("Starting web application");

    //        var builder = WebApplication.CreateBuilder(args);
    //        builder.Services.AddSerilog(); // <-- Add this line

    //        var app = builder.Build();
    //        app.MapGet("/", () => "Hello World!");

    //        app.Run();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Fatal(ex, "Application terminated unexpectedly");
    //    }
    //    finally
    //    {
    //        Log.CloseAndFlush();
    //    }
    //}
}
