namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IFieldController
    {
        IDatabase Database { get; }

        int MarketButtonsCounter { get; set; }

        void ClickedOnMine(int buttonCoordinateX, int buttonCoordinateY);

        void ClickedOnEmpty(int buttonCoordinateX, int buttonCoordinateY);

        List<bool> GetMarketButtons();

        void MarkAllMinesWithFlag();

        void TimerConfiguration(Timer timer, EventHandler e);

        void RestartGameField(int gameFieldWidth, int gameFieldHeight);

        void SolveGameField(int gameFieldWidth, int gameFieldHeight);
    }
}