namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IFieldFactory
    {
        Panel CreateGameField(int gameFieldWidth, int gameFieldHeight);
    }
}