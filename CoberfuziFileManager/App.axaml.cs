using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CoberfuziFileManager.Controllers;
using CoberfuziFileManager.Repositories;
using CoberfuziFileManager.Services;
using CoberfuziFileManager.ViewModels;
using CoberfuziFileManager.Views;

namespace CoberfuziFileManager;

public partial class App : Application
{

    private ItemController _itemController = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        // Initialize your dependencies here
        var itemRepository = new ItemRepository("Data Source=identifier.sqlite;");
        
        itemRepository.CreateTable();
        
        var itemService = new ItemService(itemRepository);
        _itemController = new ItemController(itemService); 
        
        //BindingPlugins.DataValidators.RemoveAt(0);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_itemController)
            };
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}