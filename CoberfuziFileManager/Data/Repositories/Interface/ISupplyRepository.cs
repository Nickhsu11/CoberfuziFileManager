using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface ISupplyRepository
{
    Task AddAsync(Supply supply);
    
    Task<Supply> GetByIdAsync(int id);
    
    Task UpdateAsync(Supply supply);
    
    Task<Supply> GetSupplyByIdAsync(int budgetId, int supplierID);

    Task<ICollection<Supply>> GetAllSupplysFromSupplierID(int supplierID);
}