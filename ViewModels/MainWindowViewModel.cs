using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using DemoKebabMenu.Models;

namespace DemoKebabMenu.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<KebabMenuItem> KebabItems { get; set; }
    public ObservableCollection<KebabMenuItem> KebabItems2 { get; set; }
    public RelayCommand TestCommand { get; set; }

    public MainWindowViewModel()
    {
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
            new KebabMenuItem("Menyval 3", TestCommand),
            new KebabMenuItem("Menyval 4", TestCommand),
            new KebabMenuItem("Menyval 5", TestCommand)
        };        
    }

    private void ExecuteTest()
    {
        Console.WriteLine("Test command executed!");
    }
}