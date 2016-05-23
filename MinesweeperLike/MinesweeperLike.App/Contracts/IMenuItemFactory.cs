namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    public interface IMenuItemFactory
    {
        ToolStripMenuItem CreateItem(string text, EventHandler eventHandler);
    }
}