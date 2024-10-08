using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface ISupplierRepository : IEntityRepository<Supplier>
{
    
    Task<Supplier> GetSupplierByIDAsync(int supplierId);
    
}