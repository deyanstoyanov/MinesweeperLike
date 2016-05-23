namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class FieldFactory : IFieldFactory
    {
        public Panel CreateGameField(int gameFieldWidth,
            int gameFieldHeight)
        {
            Panel newPanel = new Panel();
            newPanel.Location = new Point(FieldSettings.GameFieldLocationWidth, FieldSettings.GameFieldLocationHeight);
            //newPanel.AutoSize = true;
            newPanel.Height = (gameFieldWidth * ButtonSettings.ButtonSizeWidth) + 4;
            newPanel.Width = (gameFieldHeight * ButtonSettings.ButtonSizeHeight) + 4;
            newPanel.BorderStyle = BorderStyle.Fixed3D;

            return newPanel;
        }
    }
}