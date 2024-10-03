using Microsoft.EntityFrameworkCore;
using RentalStore.Application.Services;
using RentalStore.Domain.Interfaces;
using RentalStore.Infrastructure.Repositories;
using RentalStore.Infrastructure;
using RentalStore.SharedKernel.Dto;
using RentalStore.WebAPI.Middleware;
using RentalStore.Application.Mappings;
using FluentValidation.AspNetCore;
using FluentValidation;
using RentalStore.Application.Validators;
using NLog.Web;
using NLog;
using System.Text.Json.Serialization;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(typeof(RentalStoreMappingProfile));

    builder.Services.AddFluentValidationAutoValidation();

    var sqliteConnectionString = "Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RentalStore.db";
    builder.Services.AddDbContext<RentalStoreDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    // rejestracja walidatora
   // builder.Services.AddScoped<IValidator<CreateProductDto>, RegisterCreateProductDtoValidator>();

 
    builder.Services.AddScoped<IRentalStoreUnitOfWork, RentalStoreUnitOfWork>();
    builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
    builder.Services.AddScoped<IRentalRepository, RentalRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

    builder.Services.AddScoped<DataSeeder>();
    builder.Services.AddScoped<IEquipmentService, EquipmentService>();
    builder.Services.AddScoped<IRentalService, RentalService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ExceptionMiddleware>();

    builder.Services.AddCors(o => o.AddPolicy("RentalStore", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();

    app.UseStaticFiles();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseCors("RentalStore");

    using (var scope = app.Services.CreateScope())
    {
        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        dataSeeder.Seed();
    }

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}






