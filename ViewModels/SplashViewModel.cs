using System;

namespace PingPongDesktopApp.ViewModels
{
    /// <summary>
    /// The animated "Ping-Pong" intro. Holds no state — the view drives the
    /// animation and raises Finished when it has played through.
    /// </summary>
    public partial class SplashViewModel : ViewModelBase
    {
        /// <summary>
        /// How long the intro holds before handing over. Kept short deliberately:
        /// a splash that outstays its welcome reads as a slow app.
        /// </summary>
        public static readonly TimeSpan Duration = TimeSpan.FromSeconds(1.4);

        public event EventHandler? Finished;

        public void NotifyFinished() => Finished?.Invoke(this, EventArgs.Empty);
    }
}
