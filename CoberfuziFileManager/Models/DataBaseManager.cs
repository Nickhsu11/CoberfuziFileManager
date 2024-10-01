using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace CoberfuziFileManager.Models;

public class DataBaseManager
{
    private readonly string _connectionString;

    public DataBaseManager(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";

        if (!File.Exists(dbPath))
        {
            CreateDatabase();
        }
    }

    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
        }
        
        Console.WriteLine("DataBase created sucessfully");
    }
}