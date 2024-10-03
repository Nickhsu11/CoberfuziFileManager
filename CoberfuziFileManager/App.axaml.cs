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
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.ViewModels;
using CoberfuziFileManager.Views;
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
        services.AddScoped<IEntityRepository<Client>, ClientRepository>();
        services.AddScoped<IEntityRepository<Supplier>, SupplierRepository>();
        services.AddScoped<ClientService>();
        services.AddScoped<SupplierService>();
        services.AddScoped<EntityController>();

        services.AddSingleton<MainWindow>();
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}