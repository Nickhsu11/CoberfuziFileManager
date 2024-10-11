using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class WorkService
{

    private readonly WorkRepository _workRepository;

    public WorkService(WorkRepository workRepository)
    {
        _workRepository = workRepository;
    }

    public async Task addWorkAsync(Work work)
    {
        await _workRepository.AddAsync(work);
    }
}