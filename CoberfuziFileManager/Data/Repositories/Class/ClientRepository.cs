using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class ClientRepository : IClientRepository
{

    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Client entity)
    {
        try
        {
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx)
        {
            // Check for unique constraint violations
            if (sqliteEx.SqliteErrorCode == 19)
            {
                if (sqliteEx.Message.Contains("Nif"))
                {
                    Console.WriteLine("A Client with the given NIF already exists.");
                } 
                else if (sqliteEx.Message.Contains("Name"))
                {
                    Console.WriteLine("A Client with the given Name already exists.");
                }
                else
                {
                    Console.WriteLine("A Unique Constraint was violated.");
                }
            }
        }
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }
    
    public async Task UpdateAsync(Client entity)
    {
        _context.Clients.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Client> GetClientByIdAsync(int clientId)
    {
        return await _context.Clients.FirstOrDefaultAsync(s => s.ClientId == clientId );
    }

    /*
    public Client GetByNif(int nif)
    {
        return _context.Clients.FirstOrDefault(client => client.Nif == nif);
    }

    public bool CheckIfExists(Client entity)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Client> GetAll()
    {
        return _context.Clients.ToList();
    }


    public void Delete(int id)
    {
        var client = _context.Clients.Find(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
        else
        {
            Console.WriteLine($"Client with {id} not found. ");
        }
    }
    */

}