using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

using System;
using AutoMapper;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
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

        Console.WriteLine("====================== Entitys ( Client, Supplier ) ======================");
        Console.WriteLine("Testing Entity Controller Operations: ");

        var client = new Client
        {
            Name = "Danylo Teste1",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro cliente para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499023 ,
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
        
        
        var supplier = new Supplier
        {
            Name = "Danylo Teste3",
            Address = "Rua Bartolomeu Dias",
            Description = "Primeiro supplier para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499012 ,
            Phone = "+351930430053",
            PostCode = "2620-090",
        };
        
        var supplier2 = new Supplier
        {
            Name = "Danylo Teste4",
            Address = "Rua Bartolomeu Dias",
            Description = "Segundo supplier para testes",
            Email = "CheprazovDanylo@gmail.com",
            Nif = 508499031 ,
            Phone = "+351930430053",
            PostCode = "2620-090",
        };
        
        _entityController.AddSupplier( _mapper.Map<SupplierCompleteDTO>(supplier));
        _entityController.AddSupplier( _mapper.Map<SupplierCompleteDTO>(supplier2));

        var suppliers = await _entityController.GetSupplierById(1);
        Console.WriteLine(SupplierDTOtoString(suppliers)); 
        
        var suppliers2 = await _entityController.GetSupplierById(2);
        Console.WriteLine(SupplierDTOtoString(suppliers2));

        Console.WriteLine("\n \n");
        Console.WriteLine("====================== Work ======================");
        Console.WriteLine("\n \n");
        
        var work1 = new WorkCompleteDTO
        {
            Address = "Rua Bartolomeu Dias",
            ClientID = 1,
            PostCode = "2620-090"
        };

        await _entityController.AddWorkToClient(work1);
        
        Console.WriteLine(ClientDTOtoString(await _entityController.GetClientById(1)));
        
        Console.WriteLine("\n \n");
        Console.WriteLine("====================== Budget ======================");
        Console.WriteLine("\n \n");

        var budget1 = new BudgetCompleteDTO
        {
            Value = 100,
        };

        await _entityController.AddBudgetToWorkToClient(budget1, 1);
        Console.WriteLine(ClientDTOtoString(await _entityController.GetClientById(1)));

    }

    private string ClientDTOtoString(ClientCompleteDTO clientDTO)
    {
        if (clientDTO == null) return "Client not found.";

        var works = "";
    
        if (clientDTO.Works != null)
        {
            foreach (var currentWork in clientDTO.Works)
            {
                var budgetValue = currentWork.Budget?.Value.ToString() ?? "No Budget Assigned";  // Check for null Budget
                works += $"\n     WorkID: {currentWork.WorkID}" +
                         $"\n     Address: {currentWork.Address} \n" +
                         $"\n     Budget: {budgetValue} \n";
            }
        }
        else
        {
            works = "No works assigned.";
        }
        
        return ($"ID: {clientDTO.ClientID} " +
                $"\n Name: {clientDTO.Name} " +
                $"\n Phone: {clientDTO.Phone} " +
                $"\n Email: {clientDTO.Email} " +
                $"\n Address: {clientDTO.Address} " +
                $"\n PostCode: {clientDTO.PostCode} " +
                $"\n Description: {clientDTO.Description}" +
                $"\n Nif: {clientDTO.Nif} " +
                $"\n Works: {works}");
    }
    
    
    private string SupplierDTOtoString(SupplierCompleteDTO supplierDTO)
    {
        if (supplierDTO == null) return "Client not found.";
        
        return ($"ID: {supplierDTO.SupplierID} " +
                $"\n Name: {supplierDTO.Name} " +
                $"\n Phone: {supplierDTO.Phone} " +
                $"\n Email: {supplierDTO.Email} " +
                $"\n Address: {supplierDTO.Address} " +
                $"\n PostCode: {supplierDTO.PostCode} " +
                $"\n Description: {supplierDTO.Description}" +
                $"\n Nif: {supplierDTO.Nif} ");
    }
    
}