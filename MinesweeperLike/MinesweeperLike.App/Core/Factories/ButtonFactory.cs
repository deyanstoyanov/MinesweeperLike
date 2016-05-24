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
            GameButton button = new GameButton
                                    {
                                        Row = row, 
                                        Col = col, 
                                        LocationX = windowLocationWidth, 
                                        LocationY = windowLocationHeight, 
                                        Location = new Point(windowLocationWidth, windowLocationHeight), 
                                        Size = new Size(
                                            ButtonSettings.ButtonSizeWidth, 
                                            ButtonSettings.ButtonSizeHeight), 
                                        FlatStyle = FlatStyle.Popup, 
                                        BackColor = Color.WhiteSmoke
                                    };

            return button;
        }
    }
}