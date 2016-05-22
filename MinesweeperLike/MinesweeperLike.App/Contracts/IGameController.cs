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

        void LeftButtonOnClick(object sender, EventArgs e);

        void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs);

        void CreateButtons(Form form, MouseEventHandler mouseClick);

        void CreateLabels(Form form);

        void CreateMines();

        void CreateNumbers();
    }
}