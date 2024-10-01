using WebApplication1.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IResponseService, SequenceResponse>();

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

        var responseService = context.RequestServices.GetRequiredService<IResponseService>();
        //await context.Response.WriteAsync("Hello World");
        await context.Response.WriteAsync(await responseService.Response());

        return;
    }

    await next(context);
});

app.Run();