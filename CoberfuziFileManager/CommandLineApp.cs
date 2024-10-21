using System;
using AutoMapper;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager;

public class CommandLineApp
{
    
    private readonly EntityController _entityController;
    private readonly IMapper _mapper;

    public CommandLineApp( EntityController entityController, IMapper mapper )
    {
        _entityController = entityController ?? throw new ArgumentNullException(nameof(entityController));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
    }
    
    public void Run()
    {
        populate();
        bool exit = false; // Flag to determine if we should exit the app
    
        while (!exit) // Keeps the program in the menu loop until "Exit" is selected
        {
            Console.WriteLine("Select an option: ");
            Console.WriteLine("1. Clients ");
            Console.WriteLine("2. Works ");
            Console.WriteLine("3. Suppliers ");
            Console.WriteLine("4. Supply's ");
            Console.WriteLine("0. Exit \n");
        
            Console.Write("Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ClientsMenu(); // Go to the Clients menu
                    break;
                case "2":
                    // WorksMenu();
                    break;
                case "3":
                    // SuppliersMenu();
                    break;
                case "4":
                    // SupplysMenu();
                    break;
                case "0":
                    Console.WriteLine("Exit");
                    exit = true; // Set flag to true to exit the loop and terminate the app
                    break;
                default:
                    Console.WriteLine("Invalid Option.");
                    break;
            }
        }
        
    }

    private async void ClientsMenu()
    {
        bool backToMenu = false; // Flag to determine if we should return to the main menu

        while (!backToMenu) // Keeps the user in the Clients menu until they choose to go back
        {
            Console.WriteLine("\n=== Clients Section ===");

            var clients = await _entityController.GetAllClients(); // Fetch and display all clients
            foreach (var clientDTO in clients)
            {
                Console.WriteLine(clientDTO.ToString());
            }
        
            // Provide an option to go back to the main menu
            Console.WriteLine("\n1. Get Personal Information from Client");
            Console.WriteLine("0. Back to Main Menu \n");
            
            Console.Write("Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    backToMenu = true; // Set flag to true to break out of the loop and go back to the main menu
                    break;
                case "1":
                    GetDetailedClientInfo();
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
        }
    }

    private async void GetDetailedClientInfo()
    {
        bool BackToMainMenu = false;

        while (!BackToMainMenu)
        {
            ClientCompleteDTO client;
            String choice;
            
            Console.WriteLine("\n=== Client === ");
            Console.WriteLine("1. Give ClientID ");
            Console.WriteLine("2. Give Client Name");
            Console.WriteLine("3. Give Client NIF");
            Console.WriteLine("\n0. Back to Client Menu \n");
            
            Console.Write("Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    BackToMainMenu = true;
                    break;
                case "1":
                    
                    Console.Write("ID: ");
                    choice = Console.ReadLine();
                    
                    client = await _entityController.GetClientById(int.Parse(choice));
                    if ( client == null ) { Console.WriteLine("Invalid Client ID.");
                        break;
                    }
                    
                    Console.WriteLine(client);
                    
                    Console.WriteLine(client.ToString());
                    break;
                
                case "2":
                    
                    Console.Write("Name: ");
                    choice = Console.ReadLine();
                    
                    client = await _entityController.GetClientByName(choice);
                    if ( client == null ) { Console.WriteLine("Invalid Client Name.");
                        break;
                    }

                    Console.WriteLine(client.ToString());
                    break;
                case "3":
                    
                    Console.Write("NIF: ");
                    choice = Console.ReadLine();
                    
                    client = await _entityController.GetclientByNif(int.Parse(choice));
                    if ( client == null ) { Console.WriteLine("Invalid Client NIF.");
                        break;
                    }

                    Console.WriteLine(client.ToString());
                    break;
            }
            
        }
    }

    private async void populate()
    {
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

        var work1 = new WorkCompleteDTO
        {
            Address = "Rua Bartolomeu Dias",
            ClientID = 1,
            PostCode = "2620-090"
        };

        await _entityController.AddWorkToClient(work1);
    }
}