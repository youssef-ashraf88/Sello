using Microsoft.OpenApi;
using Sello.Api.StartupExtensions;
using Sello.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddProblemDetails();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(d => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", d)] = new List<string>()
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
