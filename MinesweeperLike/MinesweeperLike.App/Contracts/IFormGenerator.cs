namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    public interface IFormGenerator
    {
        IDatabase Database { get; }

        IMenuFactory MenuFactory { get; }

        IMenuItemFactory MenuItemFactory { get; }

        IFieldFactory FieldFactory { get; }

        IFieldGenerator FieldGenerator { get; }

        Panel GameField { get; }

        StatusStrip StatusStrip { get; }

        ToolStripStatusLabel MarketButtonsStauStatusLabel { get; }

        ToolStripStatusLabel TimerStatusLabel { get; }

        void FormSize(Form form, int width, int height);

        void CreateMenu(Form form, EventHandler eventHandler);

        void LoadStatusBar();

        void CreateGameField(
            Form form, 
            MouseEventHandler mouseEventHandler, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight);
    }
}