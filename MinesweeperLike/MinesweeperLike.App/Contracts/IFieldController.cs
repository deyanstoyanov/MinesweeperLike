namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IFieldController
    {
        IDatabase Database { get; }

        IFieldGenerator FieldGenerator { get; }

        int MarketButtonsCounter { get; set; }

        void ClickedOnMine(int buttonCoordinateX, int buttonCoordinateY);

        void ClickedOnEmpty(int buttonCoordinateX, int buttonCoordinateY);

        List<bool> GetMarketButtons();

        void MarkAllMinesWithFlag();

        void CreateGameField(
            Form form, 
            Panel panel, 
            MouseEventHandler mouseEventHandler, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight);

        void TimerConfiguration(Timer timer, EventHandler e);

        void RestartGameField();
    }
}