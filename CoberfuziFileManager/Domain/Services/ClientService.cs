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
        return await _clientRepository.GetClientByNameAsync(name.ToUpper());
    }

    public async Task<Client> GetClientByNifAsync(int nif)
    {
        return await _clientRepository.GetClientByNifAsync(nif);
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
        return await _clientRepository.GetAllClientsAsync();
    } 
    
}