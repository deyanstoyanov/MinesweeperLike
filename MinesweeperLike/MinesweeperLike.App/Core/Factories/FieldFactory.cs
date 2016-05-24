namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class FieldFactory : IFieldFactory
    {
        public Panel CreateGameField(int gameFieldWidth, int gameFieldHeight)
        {
            Panel newPanel = new Panel
                                 {
                                     Location = new Point(
                                         FieldSettings.GameFieldLocationWidth, 
                                         FieldSettings.GameFieldLocationHeight), 
                                     Height = (gameFieldWidth * ButtonSettings.ButtonSizeWidth) + 4, 
                                     Width = (gameFieldHeight * ButtonSettings.ButtonSizeHeight) + 4, 
                                     BorderStyle = BorderStyle.Fixed3D
                                 };

            return newPanel;
        }
    }
}