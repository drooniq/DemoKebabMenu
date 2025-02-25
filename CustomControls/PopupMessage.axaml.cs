using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using DemoKebabMenu.Models;

namespace DemoKebabMenu.CustomControls;
public class PopupMessage : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<PopupMessage, string>(nameof(Title));

    public static readonly StyledProperty<string> MessageProperty =
        AvaloniaProperty.Register<PopupMessage, string>(nameof(Message));
    
    public static readonly StyledProperty<MessageType> MessageTypeProperty =
        AvaloniaProperty.Register<PopupMessage, MessageType>(nameof(MessageType), defaultValue: MessageType.Info);    

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }
    
    public MessageType MessageType
    {
        get => GetValue(MessageTypeProperty);
        set => SetValue(MessageTypeProperty, value);
    }    

    public PopupMessage()
    {
        // Default background with 20% transparency
        UpdateBackgroundBasedOnType();
        PointerPressed += PopupMessage_PointerPressed;
    }

    private void PopupMessage_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (Parent is Window window)
        {
            window.Close();
        }
    }
    
    private void UpdateBackgroundBasedOnType()
    {
        Background = MessageType switch
        {
            MessageType.Error => new SolidColorBrush(Colors.DarkRed) { Opacity = 0.9 },
            MessageType.Warning => new SolidColorBrush(Colors.DarkOrange) { Opacity = 0.9 },
            MessageType.Info => new SolidColorBrush(Colors.DarkBlue) { Opacity = 0.9 },
            MessageType.Success => new SolidColorBrush(Colors.DarkGreen) { Opacity = 0.9 },
            _ => new SolidColorBrush(Colors.Black) { Opacity = 0.9 }
        };
    }
    
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == MessageTypeProperty)
        {
            UpdateBackgroundBasedOnType();
        }
    }

    public static void Show(Control caller, MessageType messageType, string title, string message)
    {
        Window? ownerWindow = FindParentWindow(caller);
        if (ownerWindow == null)
        {
            // Fallback to application's main window or first available window
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                ownerWindow = desktop.Windows.FirstOrDefault(w => w.IsActive) 
                              ?? desktop.Windows.FirstOrDefault();
            }
            
            if (ownerWindow == null)
            {
                return; // Or log a warning
            }
        }
        
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            var popup = new Window
            {
                Content = new PopupMessage
                {
                    Title = title,
                    Message = message,
                    MessageType = messageType
                },
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                Topmost = true,
                CanResize = false,
                ShowInTaskbar = false,
                Background = Brushes.Transparent,
                TransparencyLevelHint = [WindowTransparencyLevel.Transparent],
                SystemDecorations = SystemDecorations.None
            };

            popup.ShowDialog(ownerWindow);
        });
    }
    
    public static void Show(Window owner, MessageType messageType, string title, string message)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            var popup = new Window
            {
                Content = new PopupMessage
                {
                    Title = title,
                    Message = message,
                    MessageType = messageType
                },
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                Topmost = true,
                CanResize = false,
                ShowInTaskbar = false,
                Background = Brushes.Transparent,
                TransparencyLevelHint = [WindowTransparencyLevel.Transparent],
                SystemDecorations = SystemDecorations.None
            };

            popup.ShowDialog(owner);
        });
    }

    private static Window? FindParentWindow(Control control)
    {
        var parent = control.Parent;
        while (parent != null && parent is not Window)
        {
            parent = parent.Parent;
        }

        return parent as Window;
    }
}