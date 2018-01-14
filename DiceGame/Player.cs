using System;
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
            Console.WriteLine("\nType any of the given characters to select an option"
                + Environment.NewLine + "\tY - Roll Die\n\tN - Finish Turn\n\tE - End Current Game");
        }

        void ActOnPlayersChoice()
        {
            var choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.Y)
            {
                RollDie();
            }
            else if (choice == ConsoleKey.N)
            {
                FinishTurn();
            }
            else if (choice == ConsoleKey.E)
            {
                GameManager.EndGame();
            }
            else
            {
                Console.WriteLine("\nInvalid Choice!" + Environment.NewLine + "Enter a valid choice.");
                ActOnPlayersChoice();
            }
        }

        public void RollDie()
        {
            int numberRolled = Dice.GetRolledValue();
            Console.WriteLine($"\n{PlayerName} Rolled - {numberRolled}");
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
            Console.WriteLine($"\n{PlayerName} has gained {m_GameData.GainedPoints} in the current round!");
        }

        void Bust()
        {
            Console.WriteLine("\nYou got a bust!");
            ResetGameData();
            FinishTurn();
        }

        void FinishTurn()
        {
            GameManager.EndTurn(m_GameData);
        }
    }
}
