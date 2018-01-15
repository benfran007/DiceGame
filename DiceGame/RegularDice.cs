using System;
using DiceGame.Contracts;

namespace DiceGame
{
    public class RegularDice : IDice
    {
        private readonly int m_NumberOfDieSides;
        private Random m_RandomGenerator = new Random();

        public RegularDice(int numberOfDieSides)
        {
            m_NumberOfDieSides = numberOfDieSides + 1;
        }

        public int GetRolledValue()
        {
            int numberRolled = m_RandomGenerator.Next(1, m_NumberOfDieSides);
            return numberRolled;
        }
    }
}
