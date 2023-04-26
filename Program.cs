using Serilog;
using Serilog.Context;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Seq("http://localhost:5341")
        .Enrich.FromLogContext()
        .CreateLogger();

    var app = builder.Build();

    app.MapGet("/logs", () =>
    {
        // use "LogContext.PushProperty" for add extra values in context of the logs
        using var ctx = LogContext.PushProperty("SampleProp", "sample property for context");

        try
        {
            Log.Information("Hello, world!");

            // use {@} to show the objects in json format
            var sensorInput = new { Latitude = 25, Longitude = 134, Point = new { Value = 0.344432002 } };
            Log.Information("Processing {@SensorInput}", sensorInput);
            Log.Information("Procesisng {SensorInput}", sensorInput);

            // use {$} to show the types of objects
            var unknown = new[] { 1, 2, 3 };
            Log.Information("Received {$Type}", unknown);

            short x = 10, y = 0;

            // use {} to show the primitive type values
            Log.Debug("Dividing {A} by {B}", x, y);

            var z = x / y;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Something went wrong");
        }

        return "Hello World";
    });

    app.Run();
}
finally
{
    Log.CloseAndFlush();
}