using Avalonia;
using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoberfuziFileManager;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        
        if (args.Length > 0 && args[0].Equals("cli", StringComparison.OrdinalIgnoreCase))
        {
            App.BuildServiceProvider();

            // Create a scope and run the CLI app
            using (var scope = App.ServiceProvider.CreateScope())
            {
                var commandLineApp = scope.ServiceProvider.GetRequiredService<CommandLineApp>();
                commandLineApp.Run();
            }
        }
        else
        {
            // Run the application with the GUI
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        
    }
        

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}