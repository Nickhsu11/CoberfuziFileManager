using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class SupplyRepository : ISupplyRepository
{
    
    private readonly AppDbContext _context;
    
    public SupplyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Supply supply)
    {
        await _context.Supplies.AddAsync(supply);
        await _context.SaveChangesAsync();
    }

    public async Task<Supply> GetByIdAsync(int id)
    {
        return await _context.Supplies.FindAsync(id);
    }

    public async Task UpdateAsync(Supply supply)
    {
        _context.Supplies.Update(supply);
        await _context.SaveChangesAsync();
    }

    public async Task<Supply> GetSupplyByIdAsync(int supplyId)
    {
        return await _context.Supplies.FirstOrDefaultAsync(s => s.SupplyID == supplyId);
    }
    
}