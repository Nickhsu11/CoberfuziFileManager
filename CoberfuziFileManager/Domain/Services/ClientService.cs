using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class ClientService
{
    
    private readonly IEntityRepository<Client> _clientRepository;
    private readonly IDGenerator.IDGenerator _idGenerator;

    public ClientService(IEntityRepository<Client> clientRepository, IDGenerator.IDGenerator idGenerator)
    {
        _clientRepository = clientRepository;
        _idGenerator = idGenerator;
    }

    public void AddClient(Client client)
    {
        client.ClientId = _idGenerator.GetNextClientID();
        _clientRepository.Add(client);
    }

    public Client GetClientById(int id)
    {
        return _clientRepository.GetById(id);
    }
    
}