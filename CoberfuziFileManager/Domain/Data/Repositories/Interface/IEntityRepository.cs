using System.Collections.Generic;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Data.Repositories.Interface;

public interface IEntityRepository<T> where T : Entity
{
    // Add's an Entity to it's predefined table
    void Add(T entity);
    
    // Get's an Entity by it's ID and returns the Entity
    T GetById(int id);
    
    // Get's an Entity by it's NIF and returns the Entity
    T GetByNif(int nif);
    
    // Checks if there is some one with the same , name or nif
    bool CheckIfExists(T entity);
    
    // Get's all the entity's
    IEnumerable<T> GetAll();
    
    // Updates an Entity that is already in the db
    void Update(T entity);
    
    // Deletes the entity with the given ID
    void Delete(int id);
}