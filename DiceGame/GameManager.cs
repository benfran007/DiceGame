using System;
using System.Collections.Generic;
using DiceGame.Contracts;

namespace DiceGame
{
    class GameManager : IGameManager
    {
        private IList<IPlayer> players;
        private int[] m_PlayerScores;
        private IDice Dice;
        //private bool allPlayersRegistered;
        private int currentPlayerIndex;
        private int numberOfPlayers;

        public GameManager(IDice dice)
        {
            Dice = dice;
            ShowMenu();
        }

        void ShowMenu()
        {
            Console.WriteLine("  Welcome!!!\n  Are you ready to roll to victory?\n\tThe rules are simple" +
                ". The first to get to a 100 wins the game.\n\tYou can roll as long as you like," +
                " but DON'T roll a 1.\n\tA 1 is a bust and all the points gotten at that round is set to 0.\n\t");
            Console.WriteLine("\n\tN - New Game\n\tQ - Quit Game");
            GetChoice();
        }

        public void EndGame()
        {
            Console.WriteLine("The Game was exited before conclusion."
                + Environment.NewLine + "\tN - New Game\n\tQ - Quit Game");
            GetChoice();
        }

        void GetChoice()
        {
            if (Console.ReadKey().Key == ConsoleKey.N)
            {
                NewGame();
            }
            else if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                Quit();
            }
            else
            {
                Console.WriteLine("Invalid Choice!" + Environment.NewLine + "Enter a valid choice.");
                ShowMenu();
            }
        }

        void RegisterPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public void NewGame()
        {
            players = new List<IPlayer>();
            Console.WriteLine("Enter first player name...\t\t");
            string firstPlayerName = Console.ReadLine();
            RegisterPlayer(new Player(firstPlayerName, Dice, this));
            GetPlayers();
        }

        void GetPlayers()
        {
            Console.WriteLine("Enter the name of the next player...\t\t");
            string newPlayer = Console.ReadLine();
            RegisterPlayer(new Player(newPlayer, Dice, this));

            Console.WriteLine("Any more players to Add?\n\tY - Yes\n\tN - No");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                GetPlayers();
            }
            else if (Console.ReadKey().Key == ConsoleKey.N)
            {
                numberOfPlayers = players.Count;
                m_PlayerScores = new int[numberOfPlayers];
                players[currentPlayerIndex].TakeTurn();
            }
            else
            {
                Console.WriteLine("Invalid Choice!" + Environment.NewLine + "Enter a valid choice.");
                GetPlayers();
            }
        }

        public void EndTurn(GameData gameData)
        {
            m_PlayerScores[currentPlayerIndex] += gameData.GainedPoints;
            Console.WriteLine($"{players[currentPlayerIndex]} rolled {gameData.NumberOfSuccessiveRolls}" +
                $" time(s) and now has a total of {m_PlayerScores[currentPlayerIndex]} points.");

            if (m_PlayerScores[currentPlayerIndex] >= 100)
            {
                GameOver();
            }
            else
            {
                currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
                players[currentPlayerIndex].TakeTurn();
            }
        }

        public void Quit()
        {
            Console.WriteLine("Game is exiting...");
            Environment.Exit(0);
        }

        public void GameOver()
        {
            Console.WriteLine("  Game is over!\n\tThe scores are displayed below.");
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"\t\t{players[i].PlayerName}\t----\t{m_PlayerScores[i]}");
            }
            Console.WriteLine($"\t\tThe winner is {players[currentPlayerIndex].PlayerName}.\n\t\t\tCONGRATULATIONS!!!");
            ShowMenu();
        }
    }
}
