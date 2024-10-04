using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IEntityRepository<T> where T : Entity
{
    // Add's an Entity to it's predefined table
    Task AddAsync(T entity);
    
    // Get's an Entity by it's ID and returns the Entity
    Task<T> GetByIdAsync(int id);
    
    // Updates the data on the given entity for the given entity
    Task UpdateAsync(T entity);
    
}