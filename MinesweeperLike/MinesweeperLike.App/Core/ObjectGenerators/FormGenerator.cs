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

        public FormGenerator(Form form, IDatabase database, IFieldGenerator fieldGenerator)
        {
            this.form = form;
            this.Database = database;
            this.FieldGenerator = fieldGenerator;
            this.FieldFactory = new FieldFactory();
            this.MenuFactory = new MenuFactory();
            this.MenuItemFactory = new MenuItemFactory();
            this.StatusStrip = new StatusStrip();
            this.MarketButtonsStauStatusLabel = new ToolStripStatusLabel();
            this.TimerStatusLabel = new ToolStripStatusLabel();
        }

        public IDatabase Database { get; }

        public IMenuFactory MenuFactory { get; }

        public IMenuItemFactory MenuItemFactory { get; }

        public IFieldFactory FieldFactory { get; }

        public IFieldGenerator FieldGenerator { get; }

        public Panel GameField { get; private set; }

        public StatusStrip StatusStrip { get; }

        public ToolStripStatusLabel MarketButtonsStauStatusLabel { get; }

        public ToolStripStatusLabel TimerStatusLabel { get; }

        public void FormSize(Form form, int width, int height)
        {
            form.AutoSize = true;
            form.Width = (height * ButtonSettings.ButtonSizeWidth) + FieldSettings.GameFieldLocationWidth + 49;
            form.Height = (width * ButtonSettings.ButtonSizeHeight) + FieldSettings.GameFieldLocationHeight + this.StatusStrip.Size.Height + 69;
            form.FormBorderStyle = FormBorderStyle.Fixed3D;
            form.MaximizeBox = false;
        }

        public void CreateMenu(Form form, EventHandler eventHandler)
        {
            var menu = this.MenuFactory.CrateMenu("file");

            var game = this.MenuItemFactory.CreateItem("Game", null);
            var newGame = this.MenuItemFactory.CreateItem("New Game", eventHandler);
            var restart = this.MenuItemFactory.CreateItem("Restart", eventHandler);
            var solve = this.MenuItemFactory.CreateItem("Solve", eventHandler);
            var exit = this.MenuItemFactory.CreateItem("Exit", eventHandler);

            var type = this.MenuItemFactory.CreateItem("Type", null);
            var easiestType = this.MenuItemFactory.CreateItem("9x9, 10 mines", eventHandler);
            var easyType = this.MenuItemFactory.CreateItem("9x9, 35 mines", eventHandler);
            var normalType = this.MenuItemFactory.CreateItem("16x16, 40 mines", eventHandler);
            var hardNormalType = this.MenuItemFactory.CreateItem("16x16, 99 mines", eventHandler);
            var advancedType = this.MenuItemFactory.CreateItem("30x16, 130 mines", eventHandler);
            var chuckNorisType = this.MenuItemFactory.CreateItem("30x16, 170 mines", eventHandler);

            menu.Items.Add(game);
            game.DropDownItems.Add(newGame);
            game.DropDownItems.Add(restart);
            game.DropDownItems.Add(solve);
            game.DropDownItems.Add(new ToolStripSeparator());
            game.DropDownItems.Add(exit);

            menu.Items.Add(type);
            type.DropDownItems.Add(easiestType);
            type.DropDownItems.Add(easyType);
            type.DropDownItems.Add(normalType);
            type.DropDownItems.Add(hardNormalType);
            type.DropDownItems.Add(advancedType);
            type.DropDownItems.Add(chuckNorisType);

            form.Controls.Add(menu);
        }

        public void LoadStatusBar()
        {
            this.form.Controls.Add(this.StatusStrip);
            this.StatusStrip.Items.Add(this.TimerStatusLabel);
        }

        public void CreateGameField(
            Form form,
            MouseEventHandler mouseEventHandler,
            int minesCount,
            int gameFieldWidth,
            int gameFieldHeight)
        {
            this.GameField = this.FieldFactory.CreateGameField(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateButtons(this.GameField, mouseEventHandler, gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateLabels(this.GameField, gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateMines(minesCount, gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateNumbers(gameFieldWidth, gameFieldHeight);

            form.Controls.Add(this.GameField);
        }
    }
}