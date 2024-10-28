using System;
using AutoMapper;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Domain.DTOs.Supply;
using CoberfuziFileManager.Migrations;
using CoberfuziFileManager.Models;
using FluentValidation;

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
        //populate();
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
                    WorksMenu();
                    break;
                case "3":
                    SuppliersMenu();
                    break;
                case "4":
                    SupplysMenu();
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
    
    // Supply
    private async void SupplysMenu()
    {
        
        bool backToMenu = false;
        bool supplySelected = false;

        SupplierCompleteDTO currentSupplier = new SupplierCompleteDTO();
        String choice;
            
        while (!backToMenu) 
        {
            

            while (!supplySelected)
            {
                
                Console.WriteLine("\n=== Supply Section ===");
                Console.WriteLine("Start by choosing the client ID in what you will work \n");
                
                var suppliers = await _entityController.GetAllSuppliers();
                
                foreach (var supplierDTO in suppliers)
                {
                    Console.WriteLine(supplierDTO.ToString());
                }
                
                Console.Write("ID: ");
                choice = Console.ReadLine();
                    
                currentSupplier = await _entityController.GetSupplierById(int.Parse(choice));
                if ( currentSupplier != null ) {
                    Console.WriteLine($"Current Supplier set to {currentSupplier.SupplierID}.");
                    supplySelected = true;
                }
                else
                {
                    Console.WriteLine("Invalid Supplier ID.");
                }
                
            }
            
            Console.WriteLine("\n=== Supply Section ===");

            var supplys = await _entityController.GetAllSupplysFromSupplier(currentSupplier.SupplierID);
            foreach (var supplyDTO in supplys)
            {
                Console.WriteLine(supplyDTO.ToString());
            }
            
            Console.WriteLine("\n1. Get Personal Information from Work");
            Console.WriteLine("2. Add Work");
            Console.WriteLine("0. Back to Main Menu \n");
            
            Console.Write("Option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    backToMenu = true;
                    break;
                case "1":
                    //GetDetailedSupplyInfo(currentSupplier);
                    break;
                case "2":
                    AddSupply(currentSupplier);
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
        }
        
    }
    
    private async void AddSupply(SupplierCompleteDTO supplier)
    {

        bool backToMenu = false;
        
        while (!backToMenu)
        {

            SupplyCompleteDTO supplyDTO;
            String choice;
            
            Console.WriteLine("\n=== Supplt Creation Section ===");
            Console.WriteLine("To Add a new supply do the following: ");
            
            Console.Write("Name: ");
            var name = Console.ReadLine();       
            
            Console.Write("Units: ");
            var units = Console.ReadLine();
            
            Console.Write("Cost: ");
            var cost = Console.ReadLine();

            supplyDTO = new SupplyCompleteDTO
            {
                Name = name,
                Stock = 0,
                SupplierId = supplier.SupplierID,
                Units = units,
                Cost = double.Parse(cost)
            };
            
            var success = await _entityController.AddSupplyToSupplier(supplyDTO);

            if (!success)
            {
                Console.WriteLine("\nWork Creation Failed.");
    
                Console.Write("To leave the creation write 1 else 0: ");
                var input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    backToMenu = true;
                }
            }
            else
            {
                Console.WriteLine("\nWork created successfully.");
                backToMenu = true;
            }

        }
    }
    
    // Work

    private async void WorksMenu()
    {
        
        bool backToMenu = false;
        bool clientSelected = false;

        ClientCompleteDTO currentClient = new ClientCompleteDTO();
        String choice;
            
        while (!backToMenu) 
        {
            

            while (!clientSelected)
            {
                
                Console.WriteLine("\n=== Works Section ===");
                Console.WriteLine("Start by choosing the client ID in what you will work \n");
                
                var clients = await _entityController.GetAllClients();
                
                foreach (var clientDTO in clients)
                {
                    Console.WriteLine(clientDTO.ToString());
                }
                
                Console.Write("ID: ");
                choice = Console.ReadLine();
                    
                currentClient = await _entityController.GetClientById(int.Parse(choice));
                if ( currentClient != null ) {
                    Console.WriteLine($"Current client set to {currentClient.ClientID}.");
                    clientSelected = true;
                }
                else
                {
                    Console.WriteLine("Invalid Client ID.");
                }
                
            }
            
            Console.WriteLine("\n=== Works Section ===");

            var works = await _entityController.GetAllWorksFromClient(currentClient.ClientID);
            foreach (var workDTO in works)
            {
                Console.WriteLine(workDTO.ToString());
            }
            
            Console.WriteLine("\n1. Get Personal Information from Work");
            Console.WriteLine("2. Add Work");
            Console.WriteLine("0. Back to Main Menu \n");
            
            Console.Write("Option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    backToMenu = true;
                    break;
                case "1":
                    GetDetailedWorkInfo(currentClient);
                    break;
                case "2":
                    AddWork(currentClient);
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
        }
        
    }

    private async void AddWork(ClientCompleteDTO client)
    {

        bool backToMenu = false;
        
        while (!backToMenu)
        {

            WorkCompleteDTO workDTO;
            String choice;
            
            Console.WriteLine("\n=== Client Creation Section ===");
            Console.WriteLine("To Add a new client do the following: ");
            
            Console.Write("Address: ");
            var adress = Console.ReadLine();            
            
            Console.Write("PostCode: ");
            var postCode = Console.ReadLine();

            workDTO = new WorkCompleteDTO
            {
                Address = adress,
                PostCode = postCode,
                ClientID = client.ClientID,
            };
            
            var success = await _entityController.AddWorkToClient(workDTO);

            if (!success)
            {
                Console.WriteLine("\nWork Creation Failed.");
    
                Console.Write("To leave the creation write 1 else 0: ");
                var input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    backToMenu = true;
                }
            }
            else
            {
                Console.WriteLine("\nWork created successfully.");
                backToMenu = true;
            }

        }
    }
    
    private async void GetDetailedWorkInfo(ClientCompleteDTO client)
    {
        bool BackToMainMenu = false;

        while (!BackToMainMenu)
        {
            WorkCompleteDTO work;
            String choice;
            
            Console.WriteLine("\n=== Works === ");
            Console.WriteLine("1. Give WorkID ");
            Console.WriteLine("\n0. Back to Work Menu \n");
            
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
                    
                    work = await _entityController.GetWorkById(int.Parse(choice), client.ClientID);
                    if ( work == null ) { Console.WriteLine("Invalid Work ID.");
                        break;
                    }
                    
                    Console.WriteLine(work.ToString());
                    break;
                
                default :
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
            
        }
    }

    // Suppliers =====================================================================================================7
    
    private async void SuppliersMenu()
    {
        bool backToMenu = false; // Flag to determine if we should return to the main menu

        while (!backToMenu) // Keeps the user in the Clients menu until they choose to go back
        {
            Console.WriteLine("\n=== Suppliers Section ===");

            var suppliers = await _entityController.GetAllSuppliers(); // Fetch and display all clients
            foreach (var SuppliersDTO in suppliers)
            {
                Console.WriteLine(SuppliersDTO.ToString());
            }
        
            // Provide an option to go back to the main menu
            Console.WriteLine("\n1. Get Personal Information from Supplier");
            Console.WriteLine("\n2. Add Supplier");
            Console.WriteLine("0. Back to Main Menu \n");
            
            Console.Write("Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    backToMenu = true; // Set flag to true to break out of the loop and go back to the main menu
                    break;
                case "1":
                    GetDetailedSupplierInfo();
                    break;
                case "2":
                    AddSupplier();
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
        }
    }
    
    private async void AddSupplier()
    {
        bool backToMenu = false;
        
        while (!backToMenu)
        {

            SupplierCompleteDTO supplierDTO;
            String choice;
            
            Console.WriteLine("\n=== Client Creation Section ===");
            Console.WriteLine("To Add a new client do the following: ");
            
            Console.Write("Name: ");
            var name = Console.ReadLine();            
            
            Console.Write("Nif: ");
            var Nif = Console.ReadLine();
            
            Console.Write("Phone: ");
            var Phone = Console.ReadLine();
            
            Console.Write("Email: ");
            var Email = Console.ReadLine();
            
            Console.Write("Address: ");
            var Address = Console.ReadLine();
            
            Console.Write("PostCode: ");
            var PostCode = Console.ReadLine();
            
            Console.Write("Description: ");
            var Description = Console.ReadLine();

            supplierDTO = new SupplierCompleteDTO
            {
                Name = name,
                Address = Address,
                Nif = int.Parse(Nif),
                Phone = Phone,
                Email = Email,
                PostCode = PostCode,
                Description = Description
            };
            
            var success = await _entityController.AddSupplier(supplierDTO);

            if (!success)
            {
                Console.WriteLine("\nClient Creation Failed.");
    
                Console.Write("To leave the creation write 1 else 0: ");
                var input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    backToMenu = true;
                }
            }
            else
            {
                Console.WriteLine("\nClient created successfully.");
                backToMenu = true;
            }

        }
    }
    
    private async void GetDetailedSupplierInfo()
    {
        bool BackToMainMenu = false;

        while (!BackToMainMenu)
        {
            SupplierCompleteDTO supplier;
            String choice;
            
            Console.WriteLine("\n=== Supplier === ");
            Console.WriteLine("1. Give SupplierID ");
            Console.WriteLine("2. Give Client Name");
            Console.WriteLine("3. Give Client NIF");
            Console.WriteLine("\n0. Back to Supplier Menu \n");
            
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
                    
                    supplier = await _entityController.GetSupplierById(int.Parse(choice));
                    if ( supplier == null ) { Console.WriteLine("Invalid Client ID.");
                        break;
                    }
                    
                    Console.WriteLine(supplier);
                    
                    Console.WriteLine(supplier.ToString());
                    break;
                
                case "2":
                    
                    Console.Write("Name: ");
                    choice = Console.ReadLine();
                    
                    supplier = await _entityController.GetSupplierByName(choice);
                    if ( supplier == null ) { Console.WriteLine("Invalid Client Name.");
                        break;
                    }

                    Console.WriteLine(supplier.ToString());
                    break;
                case "3":
                    
                    Console.Write("NIF: ");
                    choice = Console.ReadLine();
                    
                    supplier = await _entityController.GetSupplierByNif(int.Parse(choice));
                    if ( supplier == null ) { Console.WriteLine("Invalid Client NIF.");
                        break;
                    }

                    Console.WriteLine(supplier.ToString());
                    break;
            }
            
        }
    }
    
    // Clients =======================================================================================================
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
            Console.WriteLine("\n2. Add Client");
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
                case "2":
                    AddClient();
                    break;
                default:
                    Console.WriteLine("Invalid Option. Press 0 to return to the main menu.");
                    break;
            }
        }
    }
    
    private async void AddClient()
    {
        bool backToMenu = false;
        
        while (!backToMenu)
        {

            ClientCompleteDTO clientDTO;
            String choice;
            
            Console.WriteLine("\n=== Client Creation Section ===");
            Console.WriteLine("To Add a new client do the following: ");
            
            Console.Write("Name: ");
            var name = Console.ReadLine();            
            
            Console.Write("Nif: ");
            var Nif = Console.ReadLine();
            
            Console.Write("Phone: ");
            var Phone = Console.ReadLine();
            
            Console.Write("Email: ");
            var Email = Console.ReadLine();
            
            Console.Write("Address: ");
            var Address = Console.ReadLine();
            
            Console.Write("PostCode: ");
            var PostCode = Console.ReadLine();
            
            Console.Write("Description: ");
            var Description = Console.ReadLine();

            clientDTO = new ClientCompleteDTO
            {
                Name = name,
                Address = Address,
                Nif = int.Parse(Nif),
                Phone = Phone,
                Email = Email,
                PostCode = PostCode,
                Description = Description
            };
            
            var success = await _entityController.AddClient(clientDTO);

            if (!success)
            {
                Console.WriteLine("\nClient Creation Failed.");
    
                Console.Write("To leave the creation write 1 else 0: ");
                var input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    backToMenu = true;
                }
            }
            else
            {
                Console.WriteLine("\nClient created successfully.");
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
    
    // Clients =======================================================================================================

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