using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

using System;
using AutoMapper;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoberfuziFileManager.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{

    private readonly EntityController _entityController;
    private readonly IMapper _mapper;
    
    public MainWindow(EntityController entityController, IMapper mapper)
    {
        _entityController = entityController ?? throw new ArgumentNullException(nameof(entityController));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
        InitializeComponent();
        RunConsoleTests();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void RunConsoleTests()
    {

        Console.WriteLine("Testing Entity Controller Operations: ");

        var client = new Client
        {
            Name = "Danylo Teste1",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499089 ,
            Phone = "+351930430053",
            PostCode = "2620-090",
        };

        var client2 = new Client
        {
            Name = "Danylo Teste2",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499089 ,
            Phone = "+35193043053",
            PostCode = "2620-090",
        };
        
        _entityController.AddClient( _mapper.Map<ClientCompleteDTO>(client));
        _entityController.AddClient( _mapper.Map<ClientCompleteDTO>(client2));

        var clients = await _entityController.GetClientById(1);
        Console.WriteLine(ClientDTOtoString(clients));
        
        var clients2 = await _entityController.GetClientById(2);
        Console.WriteLine(ClientDTOtoString(clients2));
    }

    private string ClientDTOtoString(ClientCompleteDTO clientDTO)
    {
        if (clientDTO == null) return "Client not found.";
        
        var works = "";
        foreach (var currentWork in clientDTO.Works)
        {
            works += $"\n WorkID: {currentWork.WorkID}" +
                     $"\n Address: {currentWork.Address} \n";
        }
        
        return ($"ID: {clientDTO.ClientID} " +
                $"\n Name: {clientDTO.Name} " +
                $"\n Phone: {clientDTO.Phone} " +
                $"\n Email: {clientDTO.Email} " +
                $"\n Address: {clientDTO.Address} " +
                $"\n PostCode: {clientDTO.PostCode} " +
                $"\n Nif: {clientDTO.Nif} " +
                $"\n Works: {works}");
    }
    
    
}