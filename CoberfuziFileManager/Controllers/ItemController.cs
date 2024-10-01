using System;
using System.Collections.Generic;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.Services;

namespace CoberfuziFileManager.Controllers;

public class ItemController
{

    private readonly ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }

    public void AddItem(string name)
    {
        _itemService.AddItem(name);
        Console.WriteLine("Item added successfully.");
    }

    public List<Item> GetItems()
    {
        return _itemService.GetItems();
    }
}