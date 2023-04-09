using System.Configuration;
using System.Net;
using System.Text.Json.Serialization;
using API.Extensions;
using API.Seetings;
using Core.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration));

builder.Services.AddControllers();
builder.Services.AddMvc().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
await builder.Services.AddApplicationServices(config);
await builder.Services.AddIdentityServices(config);

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error is not null)
        {
            context.Response.AddApplicationError(error.Error.Message);
            await context.Response.WriteAsync(error.Error.Message);
        }
    });
});

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern:"{area=exists}/{controller}/{action=Index}/{id?}");

app.MapControllers();

app.Run();