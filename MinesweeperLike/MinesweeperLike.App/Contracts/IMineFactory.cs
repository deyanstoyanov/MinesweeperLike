namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IMineFactory
    {
        void CreateMine(
            IDatabase database,
            int mineCoordinateX,
            int mineCoordinateY,
            int mineLocationX,
            int mineLocationY);
    }
}