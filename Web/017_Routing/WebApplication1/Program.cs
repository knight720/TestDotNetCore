using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheck>("My");
builder.Services.AddMvc()
    .AddMvcOptions(option => option.EnableEndpointRouting = false);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/Home", () => "Home");
app.MapPost("/Post", () => "Post");
app.MapPost("/PostValue/{value}", (string value) => $"Post: {value}");
app.MapHealthChecks("/healthz");

app.UseMvcWithDefaultRoute();

app.Run();