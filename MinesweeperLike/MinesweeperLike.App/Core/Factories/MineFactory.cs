namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Models;

    using sweeperLike.App.Constants;

    public class MineFactory : IMineFactory
    {
        public void CreateMine(IDatabase database, int mineCoordinateX, int mineCoordinateY, int mineLocationX, int mineLocationY)
        {
            Label mine = database.Labels[mineCoordinateX, mineCoordinateY];
            mine.Location = new Point(mineLocationX, mineLocationY);
            mine.Text = MineSettings.MineChar;
            mine.Font = new Font("Microsoft Sans Serif", MineSettings.MineFontSize, FontStyle.Bold, mine.Font.Unit);
            mine.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
