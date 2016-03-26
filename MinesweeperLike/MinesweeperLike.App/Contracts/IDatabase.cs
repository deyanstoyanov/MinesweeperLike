namespace MinesweeperLike.App.Contracts
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using MinesweeperLike.App.Models;

    public interface IDatabase
    {
        GameButton[,] Buttons { get; }

        int[,] GameField { get; }

        Label[,] Labels { get; }

        void AddButton(IGameButton button, int row, int col);

        void AddLabel(Label label, int row, int col);

        void AddMine(int row, int col);

        void AddNumber(int number, int row, int col);
    }
}