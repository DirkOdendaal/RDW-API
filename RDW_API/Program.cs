using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RDW_API.Configurations;
using RDW_API.Interfaces;
using RDW_API.Services;
using RDW_API.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication Configuration 
builder.Services.AddAuthentication(scheme =>
{
    scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT_Options:Issuer"],
        ValidAudience = builder.Configuration["JWT_Options:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_Options:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerCongifuration>();

// Add RDW Options
builder.Services.Configure<RDWOptions>(builder.Configuration.GetSection(RDWOptions.RDW));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.JWT));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IVehicleInfoService, VehicleInfoService>();

// Health Checks
builder.Services.AddHealthChecks();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["Azure_AppInsights:InstrumentationKey"]);

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Info API V1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
