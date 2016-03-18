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

        IEnumerable<int> RandomScores { get; }

        void CreateButtons();

        void CreateLabels();

        void GenerateRandomScores();

        void LoadRandomScoresToGameField();
    }
}