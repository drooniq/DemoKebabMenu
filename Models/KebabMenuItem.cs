using CommunityToolkit.Mvvm.Input;

namespace DemoKebabMenu.Models;

public class KebabMenuItem
{
    public string? Title { get; set; }
    public IRelayCommand? Command { get; set; }
    public object? CommandParameter { get; set; }

    public KebabMenuItem(string? title, IRelayCommand? command, object? commandParameter = null)
    {
        Title = title;
        Command = command;
        CommandParameter = commandParameter;
    }
}