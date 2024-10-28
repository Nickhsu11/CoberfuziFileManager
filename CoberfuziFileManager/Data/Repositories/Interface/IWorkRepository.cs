using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IWorkRepository
{
    Task AddAsync (Work entity);
    
    Task<Work> GetByIdAsync (int id);
    
    Task UpdateAsync(Work entity);
    
    Task<Work> GetByWorkIdAsync (int id, int clientID);

    Task<ICollection<Work>> GetAllWorksFromClientId(int ClientID);
}