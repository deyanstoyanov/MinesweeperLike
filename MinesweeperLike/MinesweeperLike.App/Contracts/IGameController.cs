namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    public interface IGameController
    {
        Button ClickedButton { get; }

        Form GameField { get; }

        IDatabase Database { get; }

        Label LabelToShow { get; }

        Timer Timer { get; }

        int Time { get; }

        void ButtonOnClick(object sender, EventArgs e);

        void LoadButtonsToGameField();

        void LoadLabelToGameField(Label label);

        void IncreaseTimer(object sender, EventArgs e);

    }
}