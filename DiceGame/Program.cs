using DiceGame.Contracts;

namespace DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IDice dice = new RegularDice(6);
            IGameManager gameManager = new GameManager(dice);
        }
    }
}
