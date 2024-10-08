using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Domain.Validatores.Client;
using CoberfuziFileManager.Models;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Controllers;

public class EntityController
{

    private readonly ClientService _clientService;
    private readonly SupplierService _supplierService;

    private readonly IValidator<ClientCompleteDTO> _clientValidator;
    private readonly IValidator<SupplierCompleteDTO> _supplierValidator;
    
    private readonly IMapper _mapper;

    public EntityController(ClientService clientService, SupplierService supplierService, 
        IValidator<ClientCompleteDTO> clientValidator, IMapper mapper, IValidator<SupplierCompleteDTO> supplierValidator)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));

        _clientValidator = clientValidator ?? throw new ArgumentNullException(nameof(clientValidator));
        _supplierValidator = supplierValidator ?? throw new ArgumentNullException(nameof(supplierValidator));
        
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
    
}