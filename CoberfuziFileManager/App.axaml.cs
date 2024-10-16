using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CoberfuziFileManager.Data;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Domain.Mappings;
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Domain.Services.IDGenerator;
using CoberfuziFileManager.Domain.Validatores.Client;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.ViewModels;
using CoberfuziFileManager.Views;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoberfuziFileManager;

public partial class App : Application
{
    
    public static IServiceProvider ServiceProvider { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IWorkRepository, WorkRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();

        services.AddScoped<IDGenerator>();
        
        services.AddScoped<ClientService>();
        services.AddScoped<SupplierService>();
        services.AddScoped<WorkService>();
        services.AddScoped<BudgetService>();
        
        services.AddScoped<EntityController>();

        services.AddAutoMapper(typeof(MappingProfile));
        
        services.AddScoped<IValidator<ClientCompleteDTO>, ClientCompleteDTOValidator>();
        services.AddScoped<IValidator<SupplierCompleteDTO>, SupplierCompleteDTOValidator>();
        services.AddScoped<IValidator<WorkCompleteDTO>, WorkCompleteDTOValidator>();
        services.AddScoped<IValidator<BudgetCompleteDTO>, BudgetCompleteDTOValidator>();

        services.AddSingleton<MainWindow>();
    }
    
    public override void OnFrameworkInitializationCompleted()
    {

        using (var scope = ServiceProvider.CreateScope())
        {
            
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureDeleted();
            Console.WriteLine("DroppedExistingTable");
            
            dbContext.Database.EnsureCreated();
            Console.WriteLine("CreatedTable");
            
        }
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}