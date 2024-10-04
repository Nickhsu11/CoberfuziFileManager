using System;
using CoberfuziFileManager.Domain.Services;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Controllers;

public class EntityController
{

    private readonly ClientService _clientService;
    private readonly SupplierService _supplierService;

    public EntityController(ClientService clientService, SupplierService supplierService)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
    
    }
    
    // Client-related methods
    public void AddClient(Client client)
    {
        _clientService.AddClientAsync(client);
    }

    public Client GetClientById(int id)
    {
        return _clientService.GetClientById(id);
    }
    
    
    // Supplier-related methods
    public void AddSupplier(Supplier supplier)
    {
        _supplierService.AddSupplier(supplier);
    }

    public Supplier GetSupplierById(int id)
    {
        return _supplierService.GetSupplierById(id);
    }
    
    // Work-related methods
    
}