using System.Collections.Generic;
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

    public async Task AddSupplyAsync(Supply supply)
    {
        supply.SupplyID = _idGenerator.GetNextSupplyID(supply);
        await _supplyRepository.AddAsync(supply);
    }

    public async Task<Supply> getSupplyBySupplyIdAsync(int supplyId, int supplierID)
    {
        return await _supplyRepository.GetSupplyByIdAsync(supplyId, supplierID);
    }
    
    public async Task AddWorkToSuply(Work work, Supply supply)
    {
        //supply.Works.Add(work);
        await _supplyRepository.UpdateAsync(supply);
    }

    public async Task<ICollection<Supply>> GetAllSupplysFromSupplierID(int supplierID)
    {
        return await _supplyRepository.GetAllSupplysFromSupplierID(supplierID);
    }
}