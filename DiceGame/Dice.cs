using System;
using DiceGame.Contracts;

namespace DiceGame
{
    class Dice : IDice
    {
        Random randomGenerator = new Random();

        public int RollDie()
        {
            int numberRolled = randomGenerator.Next(1, 7);
            return numberRolled;
        }
    }
}
