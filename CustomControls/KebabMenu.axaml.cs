using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using DemoKebabMenu.Models;

namespace DemoKebabMenu.CustomControls;

public partial class KebabMenu : TemplatedControl
{
    private Button? _kebabButton;
    private Popup? _popup;
    private ItemsControl? _menuItemsControl;

    // Size property
    public static readonly StyledProperty<double> SizeProperty =
        AvaloniaProperty.Register<KebabMenu, double>(nameof(Size), defaultValue: 32.0);

    public double Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // MenuItems property
    public static readonly StyledProperty<ObservableCollection<KebabMenuItem>> MenuItemsProperty =
        AvaloniaProperty.Register<KebabMenu, ObservableCollection<KebabMenuItem>>(
            nameof(MenuItems),
            defaultValue: new ObservableCollection<KebabMenuItem>());

    public ObservableCollection<KebabMenuItem> MenuItems
    {
        get => GetValue(MenuItemsProperty);
        set => SetValue(MenuItemsProperty, value);
    }
    
    public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
        AvaloniaProperty.Register<KebabMenu, CornerRadius>(nameof(CornerRadius), defaultValue: new CornerRadius(0));

    public new CornerRadius CornerRadius
    {
        get => GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }    

    public KebabMenu()
    {
        // Initialize with empty collection
        MenuItems = [];
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _kebabButton = e.NameScope.Find<Button>("PART_KebabButton");
        _popup = e.NameScope.Find<Popup>("PART_Popup");
        _menuItemsControl = e.NameScope.Find<ItemsControl>("PART_MenuItems");

        if (_kebabButton != null)
        {
            _kebabButton.Click += TogglePopup;
        }
    }

    private void TogglePopup(object? sender, RoutedEventArgs e)
    {
        if (_popup != null)
        {
            _popup.IsOpen = !_popup.IsOpen;
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        
        // Close popup when clicking outside
        if (_popup?.IsOpen == true && !_popup.Bounds.Contains(e.GetPosition(_popup)))
        {
            _popup.IsOpen = false;
        }
    }
}