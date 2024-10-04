using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Domain.Validatores.Client;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Controllers;

public class EntityController
{

    private readonly ClientService _clientService;
    private readonly SupplierService _supplierService;

    private readonly ClientCompleteDTOValidator _clientValidator;
    private readonly IMapper _mapper;

    public EntityController(ClientService clientService, SupplierService supplierService, 
        ClientCompleteDTOValidator clientValidator, IMapper mapper )
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));

        _clientValidator = clientValidator ?? throw new ArgumentNullException(nameof(clientValidator));
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

    public async Task<ClientCompleteDTO> GetClientById(int id)
    {
        
        var client = await _clientService.GetClientByIdAsync(id);
        if (client is null) return null;
        
        var clientDTO = _mapper.Map<ClientCompleteDTO>(client);
        return clientDTO;
        
    }
    
    
    // Supplier-related methods
    public void AddSupplier(Supplier supplier)
    {
        //_supplierService.AddSupplier(supplier);
    }

    public Supplier GetSupplierById(int id)
    {
        return null;
        //return _supplierService.GetSupplierById(id);
    }
    
    // Work-related methods
    
}