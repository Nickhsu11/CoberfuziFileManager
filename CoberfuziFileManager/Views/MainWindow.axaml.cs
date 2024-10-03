using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

using System;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoberfuziFileManager.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{

    private readonly EntityController _entityController;
    public MainWindow(EntityController entityController)
    {
        _entityController = entityController ?? throw new ArgumentNullException(nameof(entityController));
        InitializeComponent();
        RunConsoleTests();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void RunConsoleTests()
    {

        Console.WriteLine("Testing Entity Controller Operations: ");

        var client = new Client
        {
            Name = "Danylo Teste1",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499089 ,
            Phone = "+351 930 430 053",
            PostCode = "2620-090",
        };

        var client2 = new Client
        {
            Name = "Danylo Teste2",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499089 ,
            Phone = "+351 930 430 053",
            PostCode = "2620-090",
        };
        
        _entityController.AddClient(client);
        _entityController.AddClient(client2);

        var clients = _entityController.GetClientById(1);
        Console.WriteLine($" ID: {clients.Id}" +
                          $"\n Name: {clients.Name}" +
                          $"\n Phone: {clients.Phone}" +
                          $"\n Email: {clients.Email}" +
                          $"\n Address: {clients.Address}" +
                          $"\n PostCode: {clients.PostCode}" +
                          $"\n Nif: {clients.Nif}" +
                          $"\n ClientID: {clients.ClientId}"); 
        
        var clients2 = _entityController.GetClientById(2);
        
        if (clients2 != null)
        {
            Console.WriteLine($" ID: {clients2.Id}" +
                              $"\n Name: {clients2.Name}" +
                              $"\n Phone: {clients2.Phone}" +
                              $"\n Email: {clients2.Email}" +
                              $"\n Address: {clients2.Address}" +
                              $"\n PostCode: {clients2.PostCode}" +
                              $"\n Nif: {clients2.Nif}" +
                              $"\n ClientID: {clients2.ClientId}"); 
        }
        else
        {
            Console.WriteLine("The Client does not exist. ");
        }
    }
    
    
}