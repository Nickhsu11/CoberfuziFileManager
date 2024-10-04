using System;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Class;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class ClientService
{
    
    private readonly IEntityRepository<Client> _clientRepository;
    private readonly IWorkRepository _workRepository;
    
    private readonly IDGenerator.IDGenerator _idGenerator;

    public ClientService(IEntityRepository<Client> clientRepository, 
        IDGenerator.IDGenerator idGenerator, IWorkRepository workRepository)
    {
        _clientRepository = clientRepository;
        _idGenerator = idGenerator;
        _workRepository = workRepository;
    }

    public async Task AddClientAsync(Client client)
    {
        client.ClientId = _idGenerator.GetNextClientID();
        await _clientRepository.AddAsync(client);
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetByIdAsync(id);
    }

    public async Task AddWorkToClient(Work work, int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new Exception($"Client with id {clientId} was not found. ");
        }

        work.Client = client;
        client.Works.Add(work);

        await _workRepository.AddAsync(work);
        await _clientRepository.UpdateAsync(client);
    }
    
}