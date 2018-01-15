using System;
using System.Collections.Generic;
using DiceGame.Contracts;

namespace DiceGame
{
    public class GameManager : IGameManager
    {
        private IList<IPlayer> players;
        private int[] m_PlayerScores;
        private IDice Dice;
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
                " but DON'T roll a 1.\n\tA 1 is a bust and all the points gotten at that round is set to 0.\n");
            Console.WriteLine("\n\tN - New Game\n\tQ - Quit Game");
            GetChoice();
        }

        public void EndGame()
        {
            Console.WriteLine("\nThe Game was exited before conclusion."
                + Environment.NewLine + "\tN - New Game\n\tQ - Quit Game");
            GetChoice();
        }

        void GetChoice()
        {
            var choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.N)
            {
                NewGame();
            }
            else if (choice == ConsoleKey.Q)
            {
                Quit();
            }
            else
            {
                Console.WriteLine("\nInvalid Choice!" + Environment.NewLine + "Enter a valid choice.");
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
            Console.Write("\n\nEnter first player name:\t");
            string firstPlayerName = Console.ReadLine();
            RegisterPlayer(new Player(firstPlayerName, Dice, this));
            GetNextPlayer();
        }

        void GetNextPlayer()
        {
            Console.Write("\n\nEnter the name of the next player:\t");
            string newPlayer = Console.ReadLine();
            RegisterPlayer(new Player(newPlayer, Dice, this));
            GetPlayer();
        }

        void GetPlayer()
        {
            Console.WriteLine("\nAny more players to Add?\n\tY - Yes\n\tN - No");
            var choice = Console.ReadKey().Key;

            if (choice == ConsoleKey.Y)
            {
                GetNextPlayer();
            }
            else if (choice == ConsoleKey.N)
            {
                numberOfPlayers = players.Count;
                m_PlayerScores = new int[numberOfPlayers];
                SignalNextPlayer(true);
            }
            else
            {
                Console.WriteLine("\nInvalid Choice!" + Environment.NewLine + "Enter a valid choice.");
                GetPlayer();
            }
        }

        public void EndTurn(GameData gameData)
        {
            m_PlayerScores[currentPlayerIndex] += gameData.GainedPoints;
            Console.WriteLine($"\n\n{players[currentPlayerIndex].PlayerName} rolled {gameData.NumberOfSuccessiveRolls}" +
                $" time(s) and now has a total of {m_PlayerScores[currentPlayerIndex]} points.");

            if (m_PlayerScores[currentPlayerIndex] >= 100)
            {
                GameOver();
            }
            else
            {
                currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
                SignalNextPlayer(true);
            }
        }

        public void Quit()
        {
            Console.WriteLine("\nGame Is Exiting...");
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

        public void DisplayCurrentScore()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"\n\t\t{players[i].PlayerName}\t----\t{m_PlayerScores[i]}");
            }
            SignalNextPlayer(false);
        }

        void SignalNextPlayer(bool clearSuccessiveRolls)
        {
            Console.WriteLine($"\n\n{players[currentPlayerIndex].PlayerName}'s turn to play.");
            players[currentPlayerIndex].TakeTurn(clearSuccessiveRolls);
        }
    }
}
