using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Domain.DTOs.Supply;
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Domain.Validatores.Client;
using CoberfuziFileManager.Models;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Controllers;

public class EntityController
{

    private readonly ClientService _clientService;
    private readonly SupplierService _supplierService;
    private readonly WorkService _workService;
    private readonly SupplyService _supplyService;

    private readonly IValidator<ClientCompleteDTO> _clientValidator;
    private readonly IValidator<SupplierCompleteDTO> _supplierValidator;
    private readonly IValidator<WorkCompleteDTO> _workValidator;
    private readonly IValidator<BudgetCompleteDTO> _budgetValidator;
    private readonly IValidator<SupplyCompleteDTO> _supplyValidator;
    
    private readonly IMapper _mapper;

    public EntityController(ClientService clientService, SupplierService supplierService
        , WorkService workService, SupplyService supplyService
        , IValidator<ClientCompleteDTO> clientValidator, IValidator<SupplierCompleteDTO> supplierValidator
        , IValidator<WorkCompleteDTO> workValidator, IValidator<BudgetCompleteDTO> budgetValidator
        , IValidator<SupplyCompleteDTO> supplyValidator ,IMapper mapper)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
        _workService = workService ?? throw new ArgumentNullException(nameof(workService));
        _supplyService = supplyService ?? throw new ArgumentNullException(nameof(supplyService));

        _clientValidator = clientValidator ?? throw new ArgumentNullException(nameof(clientValidator));
        _supplierValidator = supplierValidator ?? throw new ArgumentNullException(nameof(supplierValidator));
        _workValidator = workValidator ?? throw new ArgumentNullException(nameof(workValidator));
        _budgetValidator = budgetValidator ?? throw new ArgumentNullException(nameof(budgetValidator));
        _supplyValidator = supplyValidator ?? throw new ArgumentNullException(nameof(supplyValidator));
        
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }
    
    // Client-related methods
    public async Task<bool> AddClient(ClientCompleteDTO client)
    {
        // Validate the client DTO
        var validationResult = await _clientValidator.ValidateAsync(client);
        if (!validationResult.IsValid)
        {
            // If validation fails, return false
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }
            return false; // Validation failed
        }

        var realClient = _mapper.Map<Client>(client);

        // Handle the addition of the client and possible exceptions
        try
        {
            await _clientService.AddClientAsync(realClient);
            return true; // Success
        }
        catch (InvalidOperationException ex) // Catch business logic exceptions like duplicates
        {
            Console.WriteLine($"Client creation failed: {ex.Message}");
            return false; // Failed due to business logic
        }
    }
    public async Task<ClientCompleteDTO> GetClientById(int clientID)
    {
        
        var client = await _clientService.GetClientByIdAsync(clientID);
        if (client is null) return null;
        
        var clientDTO = _mapper.Map<ClientCompleteDTO>(client);
        return clientDTO;
        
    }

    public async Task<ClientCompleteDTO> GetClientByName(string name)
    {

        var client = await _clientService.GetClientByNameAsync(name);
        if (client is null) return null;
        
        var clientDTO = _mapper.Map<ClientCompleteDTO>(client);
        return clientDTO;
    }

    public async Task<ClientCompleteDTO> GetclientByNif(int nif)
    {
        var client = await _clientService.GetClientByNifAsync(nif);
        if (client is null) return null;
        
        var clientDTO = _mapper.Map<ClientCompleteDTO>(client);
        return clientDTO;
    }

    public async Task<ICollection<ClientBasicDTO>> GetAllClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        var clientsDTO = new List<ClientBasicDTO>();

        foreach (var client in clients)
        {
            clientsDTO.Add(_mapper.Map<ClientBasicDTO>(client));
        }
        
        return clientsDTO;
    }
    
    // Supplier-related methods
    public async Task<bool> AddSupplier(SupplierCompleteDTO supplier)
    {
        // Validate the client DTO
        var validationResult = await _supplierValidator.ValidateAsync(supplier);
        if (!validationResult.IsValid)
        {
            // If validation fails, return false
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }
            return false; // Validation failed
        }

        var realSupplier = _mapper.Map<Supplier>(supplier);

        // Handle the addition of the client and possible exceptions
        try
        {
            await _supplierService.AddSupplierAsync(realSupplier);
            return true; // Success
        }
        catch (InvalidOperationException ex) // Catch business logic exceptions like duplicates
        {
            Console.WriteLine($"Client creation failed: {ex.Message}");
            return false; // Failed due to business logic
        }
    }

    public async Task<SupplierCompleteDTO> GetSupplierById(int SupplierID)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(SupplierID);
        if (supplier == null) return null;
        
        var supplierDTO = _mapper.Map<SupplierCompleteDTO>(supplier);
        return supplierDTO;

    }

    public async Task<SupplierCompleteDTO> GetSupplierByName(string name)
    {

        var supplier = await _supplierService.GetSupplierByNameAsync(name);
        if (supplier is null) return null;
        
        var supplierDTO = _mapper.Map<SupplierCompleteDTO>(supplier);
        return supplierDTO;
    }

    public async Task<SupplierCompleteDTO> GetSupplierByNif(int nif)
    {
        var supplier = await _supplierService.GetSupplierByNifAsync(nif);
        if (supplier is null) return null;
        
        var supplierDTO = _mapper.Map<SupplierCompleteDTO>(supplier);
        return supplierDTO;
    }
    
    public async Task<ICollection<SupplierBasicDTO>> GetAllSuppliers()
    {
        var suppliers = await _supplierService.GetAllSuppliers();
        var suppliersDTO = new List<SupplierBasicDTO>();

        foreach (var supplier in suppliers)
        {
            suppliersDTO.Add(_mapper.Map<SupplierBasicDTO>(supplier));
        }
        
        return suppliersDTO;
    }
    
    // Work-related methods
    public async Task<bool> AddWorkToClient(WorkCompleteDTO work)
    {
        // Validate the client DTO
        var validationResult = await _workValidator.ValidateAsync(work);
        if (!validationResult.IsValid)
        {
            // If validation fails, return false
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }
            return false; // Validation failed
        }

        var realWork = _mapper.Map<Work>(work);

        // Handle the addition of the client and possible exceptions
        try
        {
            await _workService.addWorkAsync(realWork);
            return true; // Success
        }
        catch (InvalidOperationException ex) // Catch business logic exceptions like duplicates
        {
            Console.WriteLine($"Client creation failed: {ex.Message}");
            return false; // Failed due to business logic
        }
    }

    // É preciso fazer uma verificação antes para ver se o Cliente existe
    public async Task<ICollection<WorkBasicDTO>> GetAllWorksFromClient(int ClientID)
    {
        var client = await _clientService.GetClientByIdAsync(ClientID);
        if (client == null)
            return null;

        var works = await _workService.GetAllWorksFromClientID(ClientID);
        var worksDTO = new List<WorkBasicDTO>();
        foreach (var work in works)
        {
            worksDTO.Add(_mapper.Map<WorkBasicDTO>(work));
        }
        
        return worksDTO;
    }

    public async Task<WorkCompleteDTO> GetWorkById(int workID, int clientID)
    {
        var work = await _workService.getWorkByWorkIdAsync(workID, clientID);
        if (work == null)
            return null;
        
        var workDTO = _mapper.Map<WorkCompleteDTO>(work);
        return workDTO;
        
    }
    
    /**
    // Budget-related methods
    public async Task AddBudgetToWorkToClient(BudgetCompleteDTO budget, int workId)
    {
        var validationResult = await _budgetValidator.ValidateAsync(budget);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }

            return;
        }
        
        var realBudget = _mapper.Map<Budget>(budget);

        var work = await _workService.getWorkByWorkIdAsync(workId);
        if (work == null)
        {
            Console.WriteLine("The WorkID introduced does not exist. ");
        }

        await _workService.AddBudgetToWork(realBudget, work);
        
    }
    */
    
    // Supply-related methods
    public async Task<bool> AddSupplyToSupplier(SupplyCompleteDTO supply)
    {
        
        // Validate the client DTO
        var validationResult = await _supplyValidator.ValidateAsync(supply);
        if (!validationResult.IsValid)
        {
            // If validation fails, return false
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }
            return false; // Validation failed
        }

        var realSupply = _mapper.Map<Supply>(supply);

        // Handle the addition of the client and possible exceptions
        try
        {
            await _supplyService.AddSupplyAsync(realSupply);
            return true; // Success
        }
        catch (InvalidOperationException ex) // Catch business logic exceptions like duplicates
        {
            Console.WriteLine($"Client creation failed: {ex.Message}");
            return false; // Failed due to business logic
        }
    }

    /*
    public async Task AddSupplyToWork(int supplyID, int workID)
    {
        var supply = await _supplyService.getSupplyBySupplyIdAsync(supplyID);
        var work = await _workService.getWorkByWorkIdAsync(workID);
        
        await _workService.AddSupplyToWork(work, supply);
        await _supplyService.AddWorkToSuply(work, supply);
    }
    */
    
    public async Task<ICollection<SupplyBasicDTO>> GetAllSupplysFromSupplier(int SupplierID)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(SupplierID);
        if (supplier == null)
            return null;

        var supplys = await _supplyService.GetAllSupplysFromSupplierID(SupplierID);
        var supplyDTO = new List<SupplyBasicDTO>();
        foreach (var supply in supplys)
        {
            supplyDTO.Add(_mapper.Map<SupplyBasicDTO>(supply));
        }
        
        return supplyDTO;
    }
    
}