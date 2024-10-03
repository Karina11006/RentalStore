using Blazored.Toast;
using Blazored.Toast.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using RentalStore.Application.Mappings;
using RentalStore.Application.Services;
using RentalStore.Application.Validators;
using RentalStore.BlazorServer.Services;
using RentalStore.Domain.Interfaces;
using RentalStore.Infrastructure;
using RentalStore.Infrastructure.Repositories;
using RentalStore.SharedKernel.Dto;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddBlazoredToast();

    builder.Services.AddAutoMapper(typeof(RentalStoreMappingProfile));

    var sqliteConnectionString = "Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RentalStore.db";
    builder.Services.AddDbContext<RentalStoreDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));


    builder.Services.AddScoped<IRentalStoreUnitOfWork, RentalStoreUnitOfWork>();
    builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
    builder.Services.AddScoped<IRentalRepository, RentalRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

    builder.Services.AddScoped<IEquipmentService, EquipmentService>();
    builder.Services.AddScoped<IRentalService, RentalService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddScoped<IToastService, ToastService>();

    builder.Services.AddScoped<IValidator<CreateRentalDto>, RegisterCreateRentalDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateRentalDto>, RegisterUpdateRentalDtoValidator>();
    builder.Services.AddScoped<IValidator<EquipmentDto>, EquipmentDtoValidator>();
    builder.Services.AddScoped<IValidator<CategoryDto>, CategoryDtoValidator>();

    builder.Services.AddFluentValidationAutoValidation();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

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
