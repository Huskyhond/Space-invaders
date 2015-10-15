using System;

namespace Game1
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
        [STAThread]
        static void Main()
        {
            var form = new Form1();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var game = new Game1())
                {
                    game.amountOfPlayers = form.amountOfPlayers;
                    game.PlayerControllerChoices = form.PlayerControllerChoices;
                    game.Run();
                }
            }
        }
    }
#endif
}
