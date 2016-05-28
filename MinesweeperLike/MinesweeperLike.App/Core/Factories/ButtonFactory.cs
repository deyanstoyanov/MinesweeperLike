namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Models;

    public class ButtonFactory : IButtonFactory
    {
        private GameButton button;

        public IGameButton CreateButton(int windowLocationWidth, int windowLocationHeight, int row, int col)
        {
            this.button = new GameButton
                              {
                                  Row = row, 
                                  Col = col, 
                                  LocationX = windowLocationWidth, 
                                  LocationY = windowLocationHeight, 
                                  Location = new Point(windowLocationWidth, windowLocationHeight), 
                                  Size = new Size(ButtonSettings.ButtonSizeWidth, ButtonSettings.ButtonSizeHeight), 
                                  FlatStyle = FlatStyle.Popup, 
                                  FlatAppearance =
                                      {
                                          MouseOverBackColor = ButtonSettings.ButtonBackColor, 
                                          BorderSize = 0
                                      }
                              };

            this.button.Paint += this.DrawThickBorder;

            return this.button;
        }

        private void DrawThickBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.button.ClientRectangle, 
                SystemColors.ControlLightLight, ButtonSettings.ButtonBorderSize, ButtonBorderStyle.Solid, 
                SystemColors.ControlLightLight, ButtonSettings.ButtonBorderSize, ButtonBorderStyle.Solid, 
                ButtonSettings.ButtonBorderColor, ButtonSettings.ButtonBorderSize, ButtonBorderStyle.Solid, 
                ButtonSettings.ButtonBorderColor, ButtonSettings.ButtonBorderSize, ButtonBorderStyle.Solid);
        }
    }
}