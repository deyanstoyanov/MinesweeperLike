namespace MinesweeperLike.App.Core.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class LabelFactory : ILabelFactory
    {
        public Label CreateLabel(int buttonLocationX, int buttonLocationY, int row, int col)
        {
            Label label = new Label();
            label.Size = new Size(LabelSettings.LabelSizeWidth, LabelSettings.LabelSizeHeight);
            label.Location = new Point(buttonLocationX, buttonLocationY);
            label.Name = string.Format("{0}-{1}", row, col);
            label.Text = string.Empty;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.BackColor = Color.LightGray;
            label.Font = new Font(FieldSettings.Font, LabelSettings.LabelFontSize, FontStyle.Bold, label.Font.Unit);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }
    }
}