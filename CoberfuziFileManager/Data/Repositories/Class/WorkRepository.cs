using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class WorkRepository : IWorkRepository
{
    
    private readonly AppDbContext _context;

    public WorkRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Work entity)
    {
        await _context.Works.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Work> GetByIdAsync(int id)
    {
        return await _context.Works.FindAsync(id);
    }
    
    public async Task UpdateAsync(Work entity)
    {
        _context.Works.Update(entity);
        await _context.SaveChangesAsync();
    }
    
}