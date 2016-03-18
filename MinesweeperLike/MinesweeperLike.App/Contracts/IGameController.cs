namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Enumerations;

    public interface IGameController
    {
        Button ClickedButton { get; }

        IDatabase Database { get; }

        GameDifficulty GameDifficulty { get; }

        Label LabelToShow { get; }

        int Score { get; }

        Timer Time { get; }

        void ButtonOnClick(object sender, EventArgs e);

        void LoadButtonsToGameField();

        void LoadLabelToGameField(Label label);
    }
}