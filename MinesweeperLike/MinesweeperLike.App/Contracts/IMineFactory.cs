namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Models;

    public interface IMineFactory
    {
        void CreateMine(IDatabase database, int mineCoordinateX, int mineCoordinateY, int mineLocationX, int mineLocationY);
    }
}
