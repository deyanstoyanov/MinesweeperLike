namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IGameButton : IButtonControl
    {
        int Row { get; }

        int Col { get; }
    }
}