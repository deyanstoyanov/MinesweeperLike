namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class MineFactory : IMineFactory
    {
        public void CreateMine(
            IDatabase database,
            Image mineImage,
            int mineCoordinateX,
            int mineCoordinateY,
            int mineLocationX,
            int mineLocationY)
        {
            Label mine = database.Labels[mineCoordinateX, mineCoordinateY];
            mine.Location = new Point(mineLocationX, mineLocationY);
            mine.Image = mineImage;
        }
    }
}