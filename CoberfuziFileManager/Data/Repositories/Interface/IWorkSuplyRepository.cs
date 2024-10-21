using System.Collections.Generic;
using System.Threading.Tasks;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IWorkSuplyRepository
{

    public Task AddWorkSupplyAsync(WorkSuply workSupply);

    public Task<List<WorkSuply>> GetSuppliesForWorkAsync(int workId);

    public Task<List<WorkSuply>> GetWorksForSupplyAsync(int supplyId);

}