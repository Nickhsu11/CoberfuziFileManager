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
        supplier.SupplierID = _idGenerator.GetNextSupplierID();
        await _supplierRepository.AddAsync(supplier);
    }

    public async Task<Supplier> GetSupplierByIdAsync(int supplierId)
    {
        return await _supplierRepository.GetSupplierByIdAsync(supplierId);
    }

    public async Task AddSupplyToSupplier(Supply supply, Supplier supplier)
    {

        supply.Supplier = supplier;
        supply.SupplierID = supplier.SupplierID;
        supplier.Supplies.Add(supply);

        await _supplyService.AddSupply(supply);
        await _supplierRepository.UpdateAsync(supplier);

    }
    
}