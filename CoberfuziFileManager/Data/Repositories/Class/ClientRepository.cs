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

    public async Task<Client> GetClientByNameAsync(string name)
    {
        return await _context.Clients.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<Client> GetClientByNifAsync(int nif)
    {
        return await _context.Clients.FirstOrDefaultAsync(s => s.Nif.Equals(nif));
    }

    public async Task<Client> GetByClientIdAsync(int clientId)
    {
        return await _context.Clients.FirstOrDefaultAsync(s => s.ClientId == clientId );
    }

    public async Task<ICollection<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }
}