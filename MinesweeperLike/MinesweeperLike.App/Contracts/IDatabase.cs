namespace MinesweeperLike.App.Contracts
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IDatabase
    {
        IGameButton[,] Buttons { get; }

        int[,] GameField { get; }

        Label[,] Labels { get; }

        void AddButton(IGameButton button, int row, int col);

        void AddLabel(Label label, int row, int col);

        void AddMine();

        void AddNumber();
    }
}