using CalMedAcad.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<CalculadoraMediaService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Blazor",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("Blazor");
app.MapControllers();

app.Run();

public partial class Program;