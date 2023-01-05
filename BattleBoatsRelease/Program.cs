using static System.Console;

namespace ExplorableWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Title = "BattleBoats";
            Game currentGame = new Game();
            currentGame.Start();
        }
    }
}