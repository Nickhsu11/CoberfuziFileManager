using System.Linq;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class BudgetRepository : IBudgetRepository
{
    
    private readonly AppDbContext _context;

    public BudgetRepository (AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Budget budget)
    {
        await _context.Budgets.AddAsync(budget);
        await _context.SaveChangesAsync();
    }

    public async Task<Budget> GetByIdAsync(int id)
    {
        return await _context.Budgets.FindAsync(id);
    }

    public async Task UpdateAsync(Budget budget)
    {
        _context.Budgets.Update(budget);
        await _context.SaveChangesAsync();
    }

    public async Task<Budget> GetBudgetIdAsync(int budgetId)
    {
        return await _context.Budgets.FirstOrDefaultAsync(b => b.BudgetId == budgetId);
    }
}