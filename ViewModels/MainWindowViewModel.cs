using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using DemoKebabMenu.CustomControls;
using DemoKebabMenu.Models;

namespace DemoKebabMenu.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<KebabMenuItem> KebabItems { get; set; }
    public ObservableCollection<KebabMenuItem> KebabItems2 { get; set; }
    public RelayCommand TestCommand { get; set; }
    
    private readonly Window _ownerWindow;    

    public MainWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow ?? throw new ArgumentNullException(nameof(ownerWindow));
        
        TestCommand = new RelayCommand(ExecuteTest);
        KebabItems = new ObservableCollection<KebabMenuItem>
        {
            new KebabMenuItem("Option 1", TestCommand),
            new KebabMenuItem("Option 2", new RelayCommand(() => Console.WriteLine("Option 2 clicked"))),
            new KebabMenuItem("Option 3", null)
        };
        
        KebabItems2 = new ObservableCollection<KebabMenuItem>
        {
            new KebabMenuItem("Lägg till Speltillfälle", TestCommand),
            new KebabMenuItem("Ta bort valt Speltillfälle", new RelayCommand(() => Console.WriteLine("Option 2 clicked"))),
            new KebabMenuItem("Error message", new RelayCommand(() => 
                PopupMessage.Show(_ownerWindow, MessageType.Error, "Missing MenuItems", "There is no menuitems to display for the popup menu."))),
            new KebabMenuItem("Warning message", new RelayCommand(() => 
                PopupMessage.Show(_ownerWindow, MessageType.Warning, "Warning MenuItems", "There is no menuitems to display for the popup menu."))),
            new KebabMenuItem("Info message", new RelayCommand(() => 
                PopupMessage.Show(_ownerWindow, MessageType.Info, "Info MenuItems", "There is no menuitems to display for the popup menu."))),
            new KebabMenuItem("Success message", new RelayCommand(()=> 
                PopupMessage.Show(_ownerWindow, MessageType.Success, "Success MenuItems", "There is no menuitems to display for the popup menu."))),
        };        
    }

    private void ExecuteTest()
    {
        Console.WriteLine("Test command executed!");
    }
}