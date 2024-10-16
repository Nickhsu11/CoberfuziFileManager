using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class WorkService
{

    private readonly IWorkRepository _workRepository;
    private readonly BudgetService _budgetService;
    
    private readonly IDGenerator.IDGenerator _idGenerator;

    public WorkService(IWorkRepository workRepository, IDGenerator.IDGenerator idGenerator
                    , BudgetService budgetService)
    {
        _workRepository = workRepository;
        _budgetService = budgetService;
        
        _idGenerator = idGenerator;
    }

    public async Task addWorkAsync(Work work)
    {
        work.WorkID = _idGenerator.GetNextWorkID();
        await _workRepository.AddAsync(work);
    }

    public async Task<Work> getWorkByWorkIdAsync(int workId)
    {
        return await _workRepository.GetByWorkIdAsync(workId);
    }

    public async Task AddBudgetToWork(Budget budget, Work work)
    {
        budget.WorkID = work.WorkID;
        budget.Work = work;
        
        work.Budget = budget;
        
        await _budgetService.AddBudgetAsync(budget);
        await _workRepository.UpdateAsync(work);
    }
}