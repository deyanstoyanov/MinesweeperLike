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
            Label label = new Label
                              {
                                  Size = new Size(LabelSettings.LabelSizeWidth, LabelSettings.LabelSizeHeight),
                                  Location = new Point(buttonLocationX, buttonLocationY),
                                  Name = $"{row}-{col}",
                                  Text = string.Empty,
                                  BorderStyle = BorderStyle.FixedSingle,
                                  BackColor = Color.LightGray
                              };
            label.Font = new Font(FieldSettings.Font, LabelSettings.LabelFontSize, FontStyle.Bold, label.Font.Unit);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }
    }
}