using System;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget);
    
    Task<Budget> GetByIdAsync(int id);
    
    Task UpdateAsync(Budget budget);
    
    Task<Budget> GetBudgetIdAsync(int budgetId);
}