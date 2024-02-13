using Serilog;
using Newtonsoft.Json;
using OpenLibrary.Api.Extensions;
using OpenLibrary.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Api.MiddleWares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using OpenLibrary.Service.Helpers;


namespace OpenLibrary.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //// Logger 
        var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        //// swagger set up
        builder.Services.AddSwaggerService();

        //// JWT 
        builder.Services.AddJwtService(builder.Configuration);

        //// Fix the Cycle
        builder.Services.AddControllers()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });


        //// Service Extension
        builder.Services.AddCustomService();

        builder.Services.AddHttpContextAccessor();

        //// Db connection
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        // Init accessor
        app.InitAccessor();

        app.UseMiddleware<ExceptionHandlerMiddleWare>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
