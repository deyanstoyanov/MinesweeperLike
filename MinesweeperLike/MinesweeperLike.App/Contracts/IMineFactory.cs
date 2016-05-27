namespace MinesweeperLike.App.Contracts
{
    using System.Drawing;
    using System.Windows.Forms;

    public interface IMineFactory
    {
        void CreateMine(
            IDatabase database,
            Image mineImage,
            int mineCoordinateX,
            int mineCoordinateY,
            int mineLocationX,
            int mineLocationY);
    }
}