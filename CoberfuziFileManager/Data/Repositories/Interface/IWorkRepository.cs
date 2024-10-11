using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IWorkRepository
{
    // Add's a work
    Task AddAsync (Work entity);
    
    // Get's the work by ID
    Task<Work> GetByIdAsync (int id);
    
    // Updates the data on the given entity for the given entity
    Task UpdateAsync(Work entity);
    
    Task<Work> GetByWorkIdAsync (int id);
}