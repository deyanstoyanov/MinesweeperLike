namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Models;

    public class ButtonFactory : IButtonFactory
    {
        public IGameButton CreateButton(int windowLocationWidth, int windowLocationHeight, int row, int col)
        {
            GameButton button = new GameButton();
            button.Row = row;
            button.Col = col;
            button.LocationX = windowLocationWidth;
            button.LocationY = windowLocationHeight;
            button.Location = new Point(windowLocationWidth, windowLocationHeight);
            button.Size = new Size(ButtonSettings.ButtonSizeWidth, ButtonSettings.ButtonSizeHeight);
            button.FlatStyle = FlatStyle.Popup;
            button.BackColor = Color.WhiteSmoke;
            button.FlatAppearance.MouseOverBackColor = button.ForeColor;

            return button;
        }
    }
}
