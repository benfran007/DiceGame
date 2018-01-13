using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceGame.Contracts;

namespace DiceGame
{
    class Player : IPlayer
    {
        private IDice Dice;
        private IGameManager GameManager;
        private string m_PlayerName;
        private GameData m_GameData;

        public Player(string playerName, IDice dice, IGameManager gameManager)
        {
            m_PlayerName = playerName;
            Dice = dice;
            GameManager = gameManager;
            m_GameData = new GameData();
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
        }

        public void TakeTurn()
        {
            ResetGameData();
            SignalPlayersTurn();
            ActOnPlayersChoice();
        }

        void ResetGameData()
        {
            m_GameData.GainedPoints = 0;
            m_GameData.NumberOfSuccessiveRolls = 0;
        }

        void SignalPlayersTurn()
        {
            Console.WriteLine("Type any of the given characters to select an option"
                + Environment.NewLine + "\tY - Roll Die\n\tN - Finish Turn\n\tE - End Current Game");
        }

        void ActOnPlayersChoice()
        {
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                RollDie();
            }
            else if (Console.ReadKey().Key == ConsoleKey.N)
            {
                FinishTurn();
            }
            else if (Console.ReadKey().Key == ConsoleKey.E)
            {
                GameManager.EndGame();
            }
            else
            {
                Console.WriteLine("Invalid Choice!" + Environment.NewLine + "Enter a valid choice.");
            }
        }

        public void RollDie()
        {
            int numberRolled = Dice.GetRolledValue();
            Console.WriteLine($"{PlayerName} Rolled - {numberRolled}");
            if (numberRolled == 1)
            {
                Bust();
            }
            else
            {
                IncreaseGamePoint(numberRolled);
                SignalPlayersTurn();
                ActOnPlayersChoice();
            }
        }

        void IncreaseGamePoint(int pointIncrement)
        {
            m_GameData.GainedPoints += pointIncrement;
        }

        void Bust()
        {
            ResetGameData();
            FinishTurn();
        }

        void FinishTurn()
        {
            GameManager.EndTurn(m_GameData, PlayerName);
        }
    }
}
