using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheck>("My");
builder.Services.AddControllers();
builder.Services.AddRazorPages();
var app = builder.Build();

#region Private Method

void Test()
{
}

// 使用 WebApplication 建立中介軟體管線
void First_Enpoint()
{
    //// Overwrite All Rule
    app.Run(async context =>
    {
        await context.Response.WriteAsync("EndPoint: Hello world!");
    });
}

// 路由的基本概念
void Routing_Basics()
{
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"custom middleware 1, endpoint: {context.GetEndpoint()?.DisplayName ?? "null"}");

        await next(context);
    });

    app.UseRouting();

    app.Use(async (context, next) =>
    {
        Console.WriteLine($"custom middleware 2, endpoint: {context.GetEndpoint()?.DisplayName ?? "null"}");

        await next(context);
    });

    app.MapGet("/", () => "Hello World!");
}

// ASP.NET Core端點定義
void Endpoint_Definition()
{
    // Location 1: before routing runs, endpoint is always null here.
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        await next(context);
    });

    app.UseRouting();

    // Location 2: after routing runs, endpoint will be non-null if routing found a match.
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        await next(context);
    });

    // Location 3: runs when this endpoint matches
    app.MapGet("/", (HttpContext context) =>
    {
        Console.WriteLine($"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        return "Hello World!";
    }).WithDisplayName("Hello");

    app.UseEndpoints(_ => { });

    // Location 4: runs after UseEndpoints - will only run if there was no match.
    app.Use(async (context, next) =>
    {
        Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        await next(context);
    });
}

void Controller()
{
    app.MapControllers();
}

void ControllerWithDefault()
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}

#endregion Private Method

// Execute
//First_Enpoint();
//Routing_Basics();
//Endpoint_Definition();
Controller();
//ControllerWithDefault(); 

app.Run();