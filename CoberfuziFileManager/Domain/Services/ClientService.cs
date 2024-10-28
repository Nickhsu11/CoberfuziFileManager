using System;
using System.Collections;
using System.Collections.Generic;
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
        var existingClient = await _clientRepository.GetEntityByNameAsync(client.Name);

        if (existingClient != null)
        {
            throw new InvalidOperationException($"Client with {client.Name} already exists");
        }

        existingClient = await _clientRepository.GetEntityByNifAsync(client.Nif);

        if (existingClient != null)
        {
            throw new InvalidOperationException($"Client with {client.Nif} already exists");
        }
        
        client.ClientId = _idGenerator.GetNextClientID();
        client.Name = client.Name.ToUpper();
        
        await _clientRepository.AddAsync(client);
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetByClientIdAsync(id);
    }

    public async Task<Client> GetClientByNameAsync(string name)
    {
        return await _clientRepository.GetEntityByNameAsync(name.ToUpper());
    }

    public async Task<Client> GetClientByNifAsync(int nif)
    {
        return await _clientRepository.GetEntityByNifAsync(nif);
    }

    public async Task AddWorkToClient(Work work, Client client)
    {
        
        work.Client = client;
        work.ClientID = client.ClientId;
        client.Works.Add(work);
        
        await _workService.addWorkAsync(work);
        await _clientRepository.UpdateAsync(client);
    }

    public async Task<ICollection<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllEntitiesAsync();
    } 
    
}