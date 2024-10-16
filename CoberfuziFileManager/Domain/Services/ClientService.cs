using System;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class ClientService
{
    
    private readonly IClientRepository _clientRepository;
    private readonly WorkService _workService;
    
    private readonly IDGenerator.IDGenerator _idGenerator;

    public ClientService(IClientRepository clientRepository, 
        IDGenerator.IDGenerator idGenerator, WorkService workService)
    {
        _clientRepository = clientRepository;
        _idGenerator = idGenerator;
        _workService = workService;
    }

    public async Task AddClientAsync(Client client)
    {
        client.ClientId = _idGenerator.GetNextClientID();
        await _clientRepository.AddAsync(client);
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetByClientIdAsync(id);
    }

    public async Task AddWorkToClient(Work work, Client client)
    {
        
        work.Client = client;
        work.ClientID = client.ClientId;
        client.Works.Add(work);
        
        await _workService.addWorkAsync(work);
        await _clientRepository.UpdateAsync(client);
    }

    public async Task AddBudgetToWorkToClient(Budget budget, Client client, int workId)
    {
        
        
    }
    
}