using System;

namespace DiceGame.Contracts
{
    public interface IGameManager
    {
        void NewGame();
        void EndGame();
        void EndTurn(GameData gameData);
        void GameOver();
        void Quit();
        void DisplayCurrentScore();
    }
}
