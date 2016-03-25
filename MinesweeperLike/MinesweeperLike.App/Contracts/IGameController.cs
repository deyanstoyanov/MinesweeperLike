namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Models;

    public interface IGameController
    {
        GameButton ClickedButton { get; }

        Form GameField { get; }

        IDatabase Database { get; }

        Label LabelToShow { get; }

        Timer Timer { get; }

        int Time { get; }

        void ButtonOnClick(object sender, EventArgs e);

        void CreateButtons(Form form);

    }
}