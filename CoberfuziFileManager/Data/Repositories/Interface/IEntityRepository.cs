using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IEntityRepository<T> where T : Entity
{
    Task AddAsync(T entity);
    
    Task<T> GetByIdAsync(int id);
    
    Task UpdateAsync(T entity);
    
    Task<T> GetClientByNameAsync(string name);
    
    Task<T> GetClientByNifAsync(int nif);
    
}