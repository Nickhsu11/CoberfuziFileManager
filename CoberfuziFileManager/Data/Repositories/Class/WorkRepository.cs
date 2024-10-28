using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Work> GetByWorkIdAsync(int workid, int clientID)
    {
        return await _context.Works
            .FirstOrDefaultAsync(w => w.ClientID == clientID && w.WorkID == workid);
    }

    public async Task<ICollection<Work>> GetAllWorksFromClientId(int ClientID)
    {
        return await _context.Works
            .Where(work => work.ClientID == ClientID)
            .ToListAsync();


    }
}