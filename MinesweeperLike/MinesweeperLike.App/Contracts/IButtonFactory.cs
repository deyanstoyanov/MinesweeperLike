namespace MinesweeperLike.App.Contracts
{
    public interface IButtonFactory
    {
        IGameButton CreateButton(int windowLocationWidth, int windowLocationheight, int row, int col);
    }
}
