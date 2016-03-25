namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IGameButton : IButtonControl
    {
        int Row { get; }

        int Col { get; }

        int LocationX { get; }

        int LocationY { get; }

        bool Visible { get; set; }
    }
}
