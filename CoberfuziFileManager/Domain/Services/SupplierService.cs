using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class SupplierService
{
    
    private readonly ISupplierRepository _supplierRepository;
    private readonly SupplyService _supplyService;
    
    private readonly IDGenerator.IDGenerator _idGenerator;

    public SupplierService(ISupplierRepository supplierRepository
        , IDGenerator.IDGenerator idGenerator, SupplyService supplyService)
    {
        _supplierRepository = supplierRepository;
        _supplyService = supplyService;
        
        _idGenerator = idGenerator;
    }

    public async Task AddSupplierAsync(Supplier supplier)
    {
        var existingClient = await _supplierRepository.GetEntityByNameAsync(supplier.Name);

        if (existingClient != null)
        {
            throw new InvalidOperationException($"Client with {supplier.Name} already exists");
        }

        existingClient = await _supplierRepository.GetEntityByNifAsync(supplier.Nif);

        if (existingClient != null)
        {
            throw new InvalidOperationException($"Client with {supplier.Nif} already exists");
        }
        
        supplier.SupplierID = _idGenerator.GetNextSupplierID();
        supplier.Name = supplier.Name.ToUpper();
        
        await _supplierRepository.AddAsync(supplier);
    }

    public async Task<Supplier> GetSupplierByIdAsync(int supplierId)
    {
        return await _supplierRepository.GetSupplierByIdAsync(supplierId);
    }
    
    public async Task<Supplier> GetSupplierByNameAsync(string name)
    {
        return await _supplierRepository.GetEntityByNameAsync(name.ToUpper());
    }

    public async Task<Supplier> GetSupplierByNifAsync(int nif)
    {
        return await _supplierRepository.GetEntityByNifAsync(nif);
    }
    
    public async Task<ICollection<Supplier>> GetAllSuppliers()
    {
        return await _supplierRepository.GetAllEntitiesAsync();
    } 

    public async Task AddSupplyToSupplier(Supply supply, Supplier supplier)
    {

        supply.Supplier = supplier;
        supply.SupplierID = supplier.SupplierID;
        supplier.Supplies.Add(supply);

        await _supplyService.AddSupplyAsync(supply);
        await _supplierRepository.UpdateAsync(supplier);

    }
    
}