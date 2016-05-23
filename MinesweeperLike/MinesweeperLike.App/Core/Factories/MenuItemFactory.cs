namespace MinesweeperLike.App.Core.Factories
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class MenuItemFactory : IMenuItemFactory
    {
        public ToolStripMenuItem CreateItem(string text, EventHandler eventHandler)
        {
            ToolStripMenuItem newItem = new ToolStripMenuItem(text: text, image: null, onClick: eventHandler);

            return newItem;
        }
    }
}