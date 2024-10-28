using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IClientRepository : IEntityRepository<Client>
{
    
    Task<Client> GetByClientIdAsync(int clientId);

}