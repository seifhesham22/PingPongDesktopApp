using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace PingPongDesktopApp.Converters
{
    /// <summary>
    /// Maps the password-visible flag to the show/hide glyph on the reveal button.
    /// </summary>
    public sealed class PasswordVisibilityConverter : IValueConverter
    {
        public static readonly PasswordVisibilityConverter Instance = new();

        private const string Visible = "🙈";
        private const string Hidden = "👁";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is true ? Visible : Hidden;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
