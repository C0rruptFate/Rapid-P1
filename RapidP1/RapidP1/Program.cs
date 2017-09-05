using System;

namespace RapidP1
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static bool shouldRestart = false;
        [STAThread]
        static void Main()
        {
            do
            {
                using (var game = new Game1())
                    game.Run();
            } while (shouldRestart);

        }
    }
#endif
}
