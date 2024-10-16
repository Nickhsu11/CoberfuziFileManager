using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class BudgetService
{

    private readonly IBudgetRepository _budgetRepository;
    private readonly IDGenerator.IDGenerator _idGenerator;

    public BudgetService(IBudgetRepository budgetRepository, IDGenerator.IDGenerator idGenerator)
    {
        _budgetRepository = budgetRepository;
        _idGenerator = idGenerator;
    }

    public async Task AddBudgetAsync(Budget budget)
    {
        budget.BudgetId = _idGenerator.GetNextBudgetID();
        await _budgetRepository.AddAsync(budget);
    }
}