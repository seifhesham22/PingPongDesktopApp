using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace PingPongDesktopApp.ViewModels
{
    /// <summary>Swaps the show/hide password glyph.</summary>
    public sealed class EyeConverter : IValueConverter
    {
        public static readonly EyeConverter Instance = new();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is true ? "🙈" : "👁";

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
