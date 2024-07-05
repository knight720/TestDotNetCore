var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Approach 1: Terminal Middleware.
app.Use(async (context, next) =>
{
    if (true)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"{context.Request.Method} {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}");
        using (var stream = new StreamReader(context.Request.Body))
        {
            var requestBody = await stream.ReadToEndAsync();
            logger.LogInformation($"{requestBody}");
        }

        await context.Response.WriteAsync("Hello World");

        return;
    }

    await next(context);
});

app.Run();