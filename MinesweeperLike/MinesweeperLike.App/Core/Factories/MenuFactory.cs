namespace MinesweeperLike.App.Core.Factories
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class MenuFactory : IMenuFactory
    {
        public MenuStrip CrateMenu(string name)
        {
            MenuStrip newMenu = new MenuStrip { Name = name };

            return newMenu;
        }
    }
}