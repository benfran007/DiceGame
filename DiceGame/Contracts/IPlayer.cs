
namespace DiceGame.Contracts
{
    public interface IPlayer
    {
        string PlayerName { get; }
        void TakeTurn(bool clearSuccessiveRolls);
        void RollDie();
    }
}
