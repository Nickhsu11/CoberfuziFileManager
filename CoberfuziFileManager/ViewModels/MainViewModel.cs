using System.Collections.Generic;
using System.IO.Enumeration;
using CoberfuziFileManager.Controllers;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.ViewModels;

using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;


public class MainViewModel : ReactiveObject
{

    private readonly ItemController _itemController;

    private string _newItemName = string.Empty;

    public string NewItemName
    {
        get => _newItemName;
        set => this.RaiseAndSetIfChanged(ref _newItemName, value);
    }

    public ObservableCollection<Item> Items { get; } = new();
    
    public ReactiveCommand<Unit, Unit> AddItemCommand { get; }

    public MainViewModel(ItemController itemController)
    {
        _itemController = itemController;

        // Initialize the Items collection
        Items = new ObservableCollection<Item>(_itemController.GetItems());

        // Define the AddItemCommand logic
        AddItemCommand = ReactiveCommand.Create(AddItem);
    }
    
    private void AddItem()
    {
        if (!string.IsNullOrEmpty(NewItemName))
        {
            _itemController.AddItem(NewItemName);
                
            // Refresh the Items collection (you might want to make this more efficient)
            Items.Clear();
            foreach (var item in _itemController.GetItems())
            {
                Items.Add(item);
            }

            // Clear the input field
            NewItemName = string.Empty;
        }
    }
    
}