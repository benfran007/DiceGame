using System;
using System.Collections.Generic;
using DiceGame.Contracts;

namespace DiceGame
{
    class GameManager : IGameManager
    {
        private List<IPlayer> players;
        private bool allPlayersRegistered;
        private int currentPlayerIndex;
        private int numberOfPlayers;

        public GameManager()
        {
            players = new List<IPlayer>();
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
                GetChoice();
            }
        }

        public void RegisterPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public void NewGame()
        {
            
        }

        public void EndTurn(GameData gameData)
        {

        }

        public void Quit()
        {
            Environment.Exit(0);
        }

        public void GameOver()
        {

        }

        public string ScoreCount()
        {
            throw new NotImplementedException();
        }
    }
}
