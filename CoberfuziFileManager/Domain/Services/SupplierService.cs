using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class SupplierService
{
    
    private readonly IEntityRepository<Supplier> _supplierRepository;

    public SupplierService(IEntityRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public void AddSupplier(Supplier supplier)
    {
        _supplierRepository.Add(supplier);
    }

    public Supplier GetSupplierById(int supplierId)
    {
        return _supplierRepository.GetById(supplierId);
    }
}