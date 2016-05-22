namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface ILabelFactory
    {
        Label CreateLabel(int buttonLocationX, int buttonLocationY, int row, int col);
    }
}