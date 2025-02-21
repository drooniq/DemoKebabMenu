namespace DemoKebabMenu.CustomControls;

using Avalonia.Data.Converters;
using System;
using System.Globalization;

public class SizeToFontSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double size)
        {
            // Adjust this multiplier based on your preference
            // 0.6 makes the text about 60% of the button size
            return size * 0.6;
        }
        return 12.0; // Default font size
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}