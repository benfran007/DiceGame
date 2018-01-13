using System;

namespace DiceGame.Contracts
{
    interface IGameManager
    {
        void RegisterPlayer(IPlayer player);
        void NewGame();
        void EndGame();
        void EndTurn(GameData gameData);
        void GameOver();
        void Quit();
        String ScoreCount();
    }
}
