using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class SupplyService
{
    
    private readonly ISupplyRepository _supplyRepository;
    
    private readonly IDGenerator.IDGenerator _idGenerator;

    public SupplyService(ISupplyRepository supplyRepository, IDGenerator.IDGenerator idGenerator)
    {
        _supplyRepository = supplyRepository;
        
        _idGenerator = idGenerator;
    }

    public async Task AddSupply(Supply supply)
    {
        supply.SupplyID = _idGenerator.GetNextSupplyID();
        await _supplyRepository.AddAsync(supply);
    }

    public async Task<Supply> getSupplyBySupplyIdAsync(int supplyId)
    {
        return await _supplyRepository.GetSupplyByIdAsync(supplyId);
    }
    
    public async Task AddWorkToSuply(Work work, Supply supply)
    {
        supply.Works.Add(work);
        await _supplyRepository.UpdateAsync(supply);
    }
}