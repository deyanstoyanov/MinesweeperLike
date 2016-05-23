namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IFieldGenerator
    {
        IDatabase Database { get; }

        IButtonFactory ButtonFactory { get; }

        ILabelFactory LabelFactory { get; }

        IMineFactory MineFactory { get; }

        Panel GameField { get; set; }

        int MinesCounter { get; }

        void CreateLabels(Panel panel, int gameFieldWidth, int gameFieldHeight);

        void CreateButtons(Panel panel, MouseEventHandler eventHandler, int gameFieldWidth, int gameFieldHeight);

        void CreateMines(int minesCount, int gameFieldWidth, int gameFieldHeight);

        void CreateNumbers(int gameFieldWidth, int gameFieldHeight);

        void ClearGameField(int gameFieldWidth, int gameFieldHeight);
    }
}