namespace MinesweeperLike.App.Core.ObjectGenerators
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;

    public class FormGenerator : IFormGenerator
    {
        private readonly Form form;

        public FormGenerator(IDatabase database, Form form)
        {
            this.form = form;
            this.Database = database;
            this.MenuFactory = new MenuFactory();
            this.MenuItemFactory = new MenuItemFactory();
        }

        public IDatabase Database { get; }

        public IMenuFactory MenuFactory { get; }

        public IMenuItemFactory MenuItemFactory { get; }

        public void FormSize(Form form, Panel panel, int width, int height)
        {
            form.AutoSize = true;
            form.Width = (height * ButtonSettings.ButtonSizeWidth) + FieldSettings.GameFieldLocationWidth + 50;
            form.Height = (width * ButtonSettings.ButtonSizeHeight) + FieldSettings.GameFieldLocationHeight + 70;
            form.FormBorderStyle = FormBorderStyle.Fixed3D;
            form.MaximizeBox = false;
        }

        public void CreateMenu(Form form, EventHandler eventHandler)
        {
            var menu = this.MenuFactory.CrateMenu("file");

            var game = this.MenuItemFactory.CreateItem("Game", null);
            var newGame = this.MenuItemFactory.CreateItem("New Game", eventHandler);
            var restart = this.MenuItemFactory.CreateItem("Restart", eventHandler);
            var exit = this.MenuItemFactory.CreateItem("Exit", eventHandler);

            var type = this.MenuItemFactory.CreateItem("Type", null);

            menu.Items.Add(game);
            game.DropDownItems.Add(newGame);
            game.DropDownItems.Add(restart);
            game.DropDownItems.Add(exit);

            menu.Items.Add(type);

            form.Controls.Add(menu);
        }

        public void LoadStatusBar()
        {
            throw new NotImplementedException();
        }
    }
}