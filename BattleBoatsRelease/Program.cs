using static System.Console;

namespace BattleBoats
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