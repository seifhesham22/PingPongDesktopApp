using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Threading.Tasks;

namespace PingPongDesktopApp.Controls
{
    /// <summary>
    /// Replays the "shake" animation on a control.
    /// </summary>
    /// <remarks>
    /// Avalonia only runs a class animation on the transition *into* that class,
    /// so replaying it means removing and re-adding the class rather than
    /// simply setting it.
    /// </remarks>
    public static class ShakeBehavior
    {
        private const string ShakeClass = "shake";
        private static readonly TimeSpan Duration = TimeSpan.FromMilliseconds(400);

        public static async Task ShakeAsync(Control target)
        {
            target.Classes.Remove(ShakeClass);

            // One frame's gap, so the removal is observed before the re-add.
            await Task.Delay(10);
            if (!target.IsAttachedToVisualTree()) return;

            target.Classes.Add(ShakeClass);

            await Task.Delay(Duration);
            if (!target.IsAttachedToVisualTree()) return;

            target.Classes.Remove(ShakeClass);
        }
    }
}
