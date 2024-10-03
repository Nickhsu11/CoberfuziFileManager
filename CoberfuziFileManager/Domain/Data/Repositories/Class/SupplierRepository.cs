using System.Collections.Generic;
using System.Linq;
using CoberfuziFileManager.Data.Repositories.Interface;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Class;

public class SupplierRepository : IEntityRepository<Supplier>
{

    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Add(Supplier entity)
    {
        _context.Suppliers.Add(entity);
        _context.SaveChanges();
    }

    public Supplier GetById(int id)
    {
        return _context.Suppliers.Find(id);
    }

    public Supplier GetByNif(int nif)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckIfExists(Supplier entity)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Supplier> GetAll()
    {
        return _context.Suppliers.ToList();
    }

    public void Update(Supplier entity)
    {
        _context.Suppliers.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
    }
    
}