using CommunityToolkit.Mvvm.Input;

namespace DemoKebabMenu.Models;

public class KebabMenuItem(string? title, IRelayCommand? command)
{
    public string? Title { get; set; } = title;
    public IRelayCommand? Command { get; set; } = command;
}