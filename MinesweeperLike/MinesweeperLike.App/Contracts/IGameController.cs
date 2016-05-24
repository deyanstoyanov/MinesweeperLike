namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Models;

    public interface IGameController
    {
        IFieldGenerator FieldGenerator { get; }

        IFormGenerator FormGenerator { get; }

        IFieldController FieldController { get; }

        Timer Timer { get; }

        void LeftButtonOnClick(object sender, MouseEventArgs mouseEventArgs);

        void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs);

        void IncreaseTIme(object sender, EventArgs e);

        void UpdateMarketButtonsCounter(int marketButtonsCount);

        void LoadMarketButtonsCounter(int minesCunter);

        void RestartGame(object sender, EventArgs e);

        void CreateNewGame(
            Form form,
            MouseEventHandler eventHandler,
            int minesCount,
            int gameFieldWidth,
            int gameFieldHeight);

        void CreateNewGameWithOtherType(
           Form form,
           IGameController gameController,
           MouseEventHandler eventHandler,
           int minesCount,
           int gameFieldWidth,
           int gameFieldHeight);
    }
}