using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
	try
	{
		await next();

	}
	catch (Exception ex)
	{
		context.Response.StatusCode = 500;
		await context.Response.WriteAsJsonAsync(new { Message = $"Something went wrong: {ex.Message}" });
	}

});
app.UseHttpsRedirection();

app.RegisterEndpointDefinitions();



app.Run();


