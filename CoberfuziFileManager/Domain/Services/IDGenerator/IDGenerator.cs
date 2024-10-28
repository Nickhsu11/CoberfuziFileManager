using System.Linq;
using CoberfuziFileManager.Data;
using CoberfuziFileManager.Models;

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

    public int GetNextWorkID(Work work)
    {
        return _context.Works
            .Where(w => w.ClientID == work.ClientID)  // Filter by ClientID
            .OrderByDescending(w => w.WorkID)         // Order by WorkID in descending order
            .Select(w => w.WorkID)                    // Select WorkID
            .FirstOrDefault() + 1;
    }

    public int GetNextBudgetID()
    {
        return _context.Budgets
            .OrderByDescending(b => b.BudgetId)
            .Select(b => b.BudgetId)
            .FirstOrDefault() + 1;
    }

    public int GetNextSupplyID(Supply supply)
    {
        return _context.Supplies
            .Where(s => s.SupplierID == supply.SupplierID)
            .OrderByDescending(s => s.SupplyID)
            .Select(s => s.SupplyID)
            .FirstOrDefault() + 1;
    }
}