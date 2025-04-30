var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8081);
});

var app = builder.Build();

app.MapGet("/temperature/{sensorId?}", (int? sensorId, string? location) =>
{
    if (sensorId.HasValue)
    {
        location = sensorId switch
        {
            1 => "Living Room",
            2 => "Bedroom",
            3 => "Kitchen",
            _ => "Unknown"
        };
    }

    var random = new Random();
    var temperature = random.NextInt64(40);

    return new
    {
        Value = temperature,
        Location = location,
        Unit = "Celsius",
        Timestamp = DateTime.UtcNow
    };
});

app.Run();
