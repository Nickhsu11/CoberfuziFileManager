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
            ClientId = 1,
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499089 ,
            Phone = "+351 930 430 053",
            PostCode = "2620-090",
        };
        
        _entityController.AddClient(client);

        var clients = _entityController.GetClientById(1);
        Console.WriteLine($" ID: {client.Id}" +
                          $"\n Name: {client.Name}" +
                          $"\n Phone: {client.Phone}" +
                          $"\n Email: {client.Email}" +
                          $"\n Address: {client.Address}" +
                          $"\n PostCode: {client.PostCode}" +
                          $"\n Nif: {client.Nif}" +
                          $"\n ClientID: {client.ClientId}"); 
    }
    
    
}