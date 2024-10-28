using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

using System;
using AutoMapper;
using CoberfuziFileManager.Domain.Controllers;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Domain.DTOs.Supply;
using CoberfuziFileManager.Models;
using CoberfuziFileManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoberfuziFileManager.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{

    private readonly EntityController _entityController;
    private readonly IMapper _mapper;
    
    public MainWindow(EntityController entityController, IMapper mapper)
    {
        _entityController = entityController ?? throw new ArgumentNullException(nameof(entityController));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
        InitializeComponent();
        //RunConsoleTests();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void RunConsoleTests()
    {

    }
    
}