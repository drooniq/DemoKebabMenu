using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoKebabMenu.ViewModels;

namespace DemoKebabMenu.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
    }
}