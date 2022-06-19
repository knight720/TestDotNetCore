using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheck>("My");
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapHealthChecks("/healthz");

app.Run();