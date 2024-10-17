using System;
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
    public async void AddClient(ClientCompleteDTO client)
    {
        
        var validationResult = await _clientValidator.ValidateAsync(client);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }

            return;
        }
        
        var realClient = _mapper.Map<Client>(client);
        await _clientService.AddClientAsync(realClient);
        
    }

    public async Task<ClientCompleteDTO> GetClientById(int clientID)
    {
        
        var client = await _clientService.GetClientByIdAsync(clientID);
        if (client is null) return null;
        
        var clientDTO = _mapper.Map<ClientCompleteDTO>(client);
        return clientDTO;
        
    }
    
    // Supplier-related methods
    public async void AddSupplier(SupplierCompleteDTO supplier)
    {
        var validationResult = await _supplierValidator.ValidateAsync(supplier);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }

            return;
        }
        
        var realSupplier = _mapper.Map<Supplier>(supplier);
        await _supplierService.AddSupplierAsync(realSupplier);
    }

    public async Task<SupplierCompleteDTO> GetSupplierById(int SupplierID)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(SupplierID);
        if (supplier == null) return null;
        
        var supplierDTO = _mapper.Map<SupplierCompleteDTO>(supplier);
        return supplierDTO;

    }
    
    // Work-related methods
    public async Task AddWorkToClient(WorkCompleteDTO work)
    {
        var validationResult = await _workValidator.ValidateAsync(work);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }

            return;
        }
        
        var realWork = _mapper.Map<Work>(work);
        
        var client = await _clientService.GetClientByIdAsync(realWork.ClientID);
        if (client == null)
        {
            Console.WriteLine("The ClientID introduced does not exist. ");
        }
        
        await _clientService.AddWorkToClient(realWork, client);
    }
    
    
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
    
    // Supply-related methods
    public async Task AddSupplyToSupplier(SupplyCompleteDTO supply, int supplierID)
    {
        var validationResult = await _supplyValidator.ValidateAsync(supply);
        if (!validationResult.IsValid)
        {
            foreach (var failure in validationResult.Errors)
            {
                Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
            }

            return;
        }
        
        var realSupply = _mapper.Map<Supply>(supply);
        
        var supplier = await _supplierService.GetSupplierByIdAsync(supplierID);
        if (supplier == null)
        {
            Console.WriteLine("The Supplier ID introduced does not exist. ");
            return;
        }

        await _supplierService.AddSupplyToSupplier(realSupply, supplier);
    }

    public async Task AddSupplyToWork(int supplyID, int workID)
    {
        var supply = await _supplyService.getSupplyBySupplyIdAsync(supplyID);
        var work = await _workService.getWorkByWorkIdAsync(workID);
        
        await _workService.AddSupplyToWork(work, supply);
        await _supplyService.AddWorkToSuply(work, supply);
    }
    
}