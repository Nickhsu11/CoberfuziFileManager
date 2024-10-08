using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class SupplierService
{
    
    private readonly ISupplierRepository _supplierRepository;
    private readonly IDGenerator.IDGenerator _idGenerator;

    public SupplierService(ISupplierRepository supplierRepository, IDGenerator.IDGenerator idGenerator)
    {
        _supplierRepository = supplierRepository;
        _idGenerator = idGenerator;
    }

    public async Task AddSupplierAsync(Supplier supplier)
    {
        supplier.SupplierID = _idGenerator.GetNextSupplierID();
        await _supplierRepository.AddAsync(supplier);
    }

    public async Task<Supplier> GetSupplierByIdAsync(int supplierId)
    {
        return await _supplierRepository.GetSupplierByIDAsync(supplierId);
    }
    
}