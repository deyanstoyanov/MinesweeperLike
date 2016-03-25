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

        void MouseClick(object sender, MouseEventArgs e);

        void CreateButtons(Form form);

        void CreateLabels(Form form);
    }
}