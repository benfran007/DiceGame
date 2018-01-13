using System;

namespace DiceGame.Contracts
{
    interface IGameManager
    {
        void NewGame();
        void EndGame();
        void EndTurn(GameData gameData, string currentPlayer);
        void GameOver();
        void Quit();
        String ScoreCount();
    }
}
