namespace MinesweeperLike.App.Contracts
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IDatabase
    {
        Button[,] Buttons { get; }

        int[,] GameField { get; }

        Label[,] Labels { get; }

        IEnumerable<IMine> Mines { get; }

        void AddButtons();

        void AddLabels(Form form);

        void AddMines();

        void AddNumbers();
    }
}