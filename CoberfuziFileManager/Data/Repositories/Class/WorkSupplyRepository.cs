using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class WorkSupplyRepository : IWorkSuplyRepository
{
    
    private readonly AppDbContext _context;

    public WorkSupplyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddWorkSupplyAsync(WorkSuply workSupply)
    {
        await _context.WorkSuplies.AddAsync(workSupply);
        await _context.SaveChangesAsync();
    }

    public async Task<List<WorkSuply>> GetSuppliesForWorkAsync(int workId)
    {
        return await _context.WorkSuplies
            .Where(ws => ws.WorkID == workId)
            .Include(ws => ws.Supply)
            .ToListAsync();
    }

    public async Task<List<WorkSuply>> GetWorksForSupplyAsync(int supplyId)
    {
        return await _context.WorkSuplies
            .Where(ws => ws.SupplyID == supplyId)
            .Include(ws => ws.Work)
            .ToListAsync();
    }
    
}