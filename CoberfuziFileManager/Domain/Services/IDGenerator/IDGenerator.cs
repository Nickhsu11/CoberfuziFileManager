using System.Linq;
using CoberfuziFileManager.Data;

namespace CoberfuziFileManager.Domain.Services.IDGenerator;

public class IDGenerator
{
    
    private readonly AppDbContext _context;

    public IDGenerator(AppDbContext context)
    {
        _context = context;
    }

    public int GetNextClientID()
    {
        return _context.Clients
            .OrderByDescending(c => c.ClientId)
            .Select(c => c.ClientId)
            .FirstOrDefault() + 1;
    }

    public int GetNextSupplierID()
    {
        return _context.Suppliers
            .OrderByDescending(s => s.SupplierID)
            .Select(s => s.SupplierID)
            .FirstOrDefault() + 1;
    }

    public int GetNextWorkID()
    {
        return _context.Works
            .OrderByDescending(w => w.WorkID)
            .Select(w => w.WorkID)
            .FirstOrDefault() + 1;
    }

    public int GetNextBudgetID()
    {
        return _context.Budgets
            .OrderByDescending(b => b.BudgetId)
            .Select(b => b.BudgetId)
            .FirstOrDefault() + 1;
    }

    public int GetNextSupplyID()
    {
        return _context.Supplies
            .OrderByDescending(s => s.SupplyID)
            .Select(s => s.SupplyID)
            .FirstOrDefault() + 1;
    }
}