
namespace DiceGame.Contracts
{
    interface IPlayer
    {
        string PlayerName { get; }
        void TakeTurn(bool clearSuccessiveRolls);
        void RollDie();
    }
}
