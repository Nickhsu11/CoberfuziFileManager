using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Services;

public class ClientService
{
    
    private readonly IEntityRepository<Client> _clientRepository;

    public ClientService(IEntityRepository<Client> clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public void AddClient(Client client)
    {
        _clientRepository.Add(client);
    }

    public Client GetClientById(int id)
    {
        return _clientRepository.GetById(id);
    }
    
}