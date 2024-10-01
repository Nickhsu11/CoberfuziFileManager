using System;
using System.Collections.Generic;
using CoberfuziFileManager.Models;
using Microsoft.Data.Sqlite;

namespace CoberfuziFileManager.Repositories;

public class ItemRepository
{

    // String that represents the path to the db
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemRepository"/> class with the specified connection string.
    /// </summary>
    /// <param name="connectionString"> The connection string used to connect to the database.</param>
    public ItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Creates the `ITEM` table in the database if it does not already exist.
    /// </summary>
    /// <remarks>
    /// This method sets up a connection to the database using the connection string provided.
    /// It then executes a SQL command to create the `ITEM` table with columns for `Id` and `Name`.
    /// The `Id` column is an integer primary key that autoincrements, and the `Name` column is a non-null text field.
    /// </remarks>
    public void CreateTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Items (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                )";

        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Adds a new item to the `Items` table in the database.
    /// </summary>
    /// <param name="item">The item to be added to the database.</param>
    /// <exception cref="InvalidOperationException"> Thrown when the last inserted row ID cannot be retrieved.</exception>
    /// <remarks>
    /// This method sets up a connection to the database using the connection string provided.
    /// It then executes a SQL command to insert a new item into the `Items` table.
    /// After the insertion, it retrieves the last inserted row ID and assigns it to the `Id` property of the item.
    /// If the row ID cannot be retrieved, an <see cref="InvalidOperationException"/> is thrown.
    /// </remarks>
    public void AddItem(Item item)
    {
        // Setting up of the connection for the db
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Creates the command to insert an Item into the db
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Items (Name) VALUES ($name)";
        command.Parameters.AddWithValue("$name", item.Name);
        command.ExecuteNonQuery();

        // Getting the number of the last rowID
        using var commandLastId = connection.CreateCommand();
        commandLastId.CommandText = "SELECT last_insert_rowid()";

        // Checks if the number that was retrieved was not null
        var result = commandLastId.ExecuteScalar();
        if (result != null)
        {
            item.Id = Convert.ToInt64(result);
        }
        else
        {
            throw new InvalidOperationException("Failed to retrieve the last inserted row ID.");
        }
    }

    /// <summary>
    /// Retrieves all items from the `Items` table in the database.
    /// </summary>
    /// <returns>A list of all items in the database.</returns>
    /// <remarks>
    /// This method sets up a connection to the database using the connection string provided.
    /// It then executes a SQL command to select all items from the `Items` table.
    /// The method reads each row returned and adds the items to a list, which is then returned.
    /// </remarks>
    public List<Item> GetItems()
    {
            
        // Creates a list with all the items
        var items = new List<Item>();

        // Connects with the db
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Creates the command to select all the Items
        using var command = connection.CreateCommand();
        command.CommandText = "Select Id, Name FROM Items";

        // Executes the reader and reads the file and adds the Items
        // to the List
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(CreateItemFromReader(reader));
        }

        return items;
    }

    /// <summary>
    /// Creates an <see cref="Item"/> instance from the given <see cref="SqliteDataReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="SqliteDataReader"/> used to read the item data.</param>
    /// <returns>An <see cref="Item"/> instance populated with data from the reader.</returns>
    /// <remarks>
    /// This method extracts the `Id` and `Name` fields from the <see cref="SqliteDataReader"/>
    /// and uses them to create and return a new <see cref="Item"/> instance.
    /// </remarks>
    private Item CreateItemFromReader(SqliteDataReader reader)
    {
        return new Item
        {
            Id = reader.GetInt64(0),
            Name = reader.GetString(1)
        };
    }
}