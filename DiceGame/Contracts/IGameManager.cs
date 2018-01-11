using System;

namespace DiceGame.Contracts
{
    interface IGameManager
    {
        void NewGame();
        void EndGame();
        String ScoreCount();
    }
}
