using System.Collections.Generic;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.Repositories;

namespace CoberfuziFileManager.Services;

public class ItemService
{
    private readonly ItemRepository _itemRepository;

    public ItemService(ItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public void InitializeDatabase()
    {
        _itemRepository.CreateTable();
    }

    public void AddItem(string name)
    {
        var item = new Item { Name = name };
        _itemRepository.AddItem(item);
    }

    public List<Item> GetItems()
    {
        return _itemRepository.GetItems();
    }
    
}