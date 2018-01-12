
namespace DiceGame.Contracts
{
    interface IPlayer
    {
        string PlayerName { get; }
        void TakeTurn();
        void RollDie();
    }
}
