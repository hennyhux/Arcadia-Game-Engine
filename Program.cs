using GameSpace;
using System;

namespace FirstGame
{
    public static class Program
    {
        [STAThread]
        private static void Main()
       {
            using (GameRoot game = new GameRoot())
            {
                game.Run();
            }
        }
    }
}
