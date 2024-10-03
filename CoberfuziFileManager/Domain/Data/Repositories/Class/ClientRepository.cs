using System;
using System.Collections.Generic;
using System.Linq;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class ClientRepository : IEntityRepository<Client>
{

    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Client entity)
    {
        try
        {
            _context.Clients.Add(entity);
            _context.SaveChanges();
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

    public Client GetById(int id)
    {
        return _context.Clients.Find(id);
    }

    public Client GetByNif(int nif)
    {
        return _context.Clients.Find(nif);
    }

    public bool CheckIfExists(Client entity)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Client> GetAll()
    {
        return _context.Clients.ToList();
    }

    public void Update(Client entity)
    {
        _context.Clients.Update(entity);
    }

    public void Delete(int id)
    {
        var client = _context.Clients.Find(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
    }

}