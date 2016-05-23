﻿namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    public interface IFormGenerator
    {
        IDatabase Database { get; }

          IMenuFactory MenuFactory { get; }

        IMenuItemFactory MenuItemFactory { get; }

        StatusStrip StatusStrip { get; }

        ToolStripStatusLabel MarketButtonsStauStatusLabel { get; }

        ToolStripStatusLabel TimerStatusLabel { get; }

        void FormSize(Form form, Panel panel, int width, int height);


        void CreateMenu(Form form, EventHandler eventHandler);

        void LoadStatusBar();
    }
}