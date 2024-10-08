using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class SupplierRepository : ISupplierRepository
{

    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Supplier entity)
    {

        try
        {
            await _context.Suppliers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqliteEx)
        {
            // Check for unique constraint violations
            if (sqliteEx.SqliteErrorCode == 19)
            {
                if (sqliteEx.Message.Contains("Nif"))
                {
                    Console.WriteLine("A Supplier with the given NIF already exists.");
                } 
                else if (sqliteEx.Message.Contains("Name"))
                {
                    Console.WriteLine("A Supplier with the given Name already exists.");
                }
                else
                {
                    Console.WriteLine("A Unique Constraint was violated.");
                }
            }
        }
    }
    
    public async Task<Supplier> GetByIdAsync(int id)
    {
        return await _context.Suppliers.FindAsync(id);
    }
    
    public async Task UpdateAsync(Supplier entity)
    {
        _context.Suppliers.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Supplier> GetSupplierByIDAsync(int supplierId)
    {
        return await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == supplierId);
    }

    /*
    public Supplier GetByNif(int nif)
    {
        return _context.Suppliers.FirstOrDefault(supplier => supplier.Nif == nif);
    }

    public bool CheckIfExists(Supplier entity)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Supplier> GetAll()
    {
        return _context.Suppliers.ToList();
    }

    
    public void Delete(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
        else
        {
            Console.WriteLine($"The Supplier with the ID {id} was not found.");
        }
    }
    */
    
    
}