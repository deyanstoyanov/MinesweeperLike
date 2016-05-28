namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class FieldFactory : IFieldFactory
    {
        private Panel panel;

        public Panel CreateGameField(int gameFieldWidth, int gameFieldHeight)
        {
            this.panel = new Panel
                                {
                                    Location = new Point( 
                                        FieldSettings.GameFieldLocationWidth, 
                                        FieldSettings.GameFieldLocationHeight), 
                                    Height = (gameFieldWidth * ButtonSettings.ButtonSizeWidth) + 8, 
                                    Width = (gameFieldHeight * ButtonSettings.ButtonSizeHeight) + 8, 
                                    BorderStyle = BorderStyle.None
                                };

            this.panel.Paint += this.DrawThickBorder;

            return this.panel;
        }

        private void DrawThickBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel.ClientRectangle, 
                FieldSettings.GameFieldBorderColor, FieldSettings.GameFieldBorderSize, ButtonBorderStyle.Solid, 
                FieldSettings.GameFieldBorderColor, FieldSettings.GameFieldBorderSize, ButtonBorderStyle.Solid, 
                SystemColors.ControlLightLight, FieldSettings.GameFieldBorderSize, ButtonBorderStyle.Solid, 
                SystemColors.ControlLightLight, FieldSettings.GameFieldBorderSize, ButtonBorderStyle.Solid);
        }
    }
}