namespace MinesweeperLike.App.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;
    using MinesweeperLike.App.Core.ObjectGenerators;
    using MinesweeperLike.App.Data;
    using MinesweeperLike.App.Models;

    public class GameController : IGameController
    {
        private GameButton clickedButton;

        private bool dead;

        public GameController(Form form, int gameFieldWidth, int gameFieldHeight)
        {
            this.Form = form;
            this.Database = new Database(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator = new FieldGenerator(this.Database);
            this.GameFormGenerator = new FormGenerator(this.Database, this.Form);
            this.FieldController = new FieldController(this.Database, this.FieldGenerator);
            this.clickedButton = new GameButton();
            this.Timer = new Timer();
        }

        public IDatabase Database { get; }

        public IFieldGenerator FieldGenerator { get; }

        public IFormGenerator GameFormGenerator { get; }

        public IFieldController FieldController { get; }

        public Form Form { get; }

        public Timer Timer { get; private set; }

        public Panel Panel { get; }

        public int Time { get; private set; }

        public void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.dead)
            {
                return;
            }

            this.clickedButton = sender as GameButton;

            if (this.clickedButton.Text != string.Empty)
            {
                this.clickedButton.Text = string.Empty;
                return;
            }

            this.clickedButton.Text = FieldSettings.FlagChar;
            this.clickedButton.ForeColor = Color.Red;
            this.clickedButton.Font = new Font(FieldSettings.Font, 8);
            this.clickedButton.TextAlign = ContentAlignment.MiddleCenter;
        }

        public void LeftButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            this.clickedButton = sender as GameButton;

            if (this.dead || this.clickedButton.Text != string.Empty)
            {
                return;
            }

            int buttonCoordinateX = this.clickedButton.Row;
            int buttonCoordinateY = this.clickedButton.Col;

            if (this.Database.GameField[buttonCoordinateX, buttonCoordinateY] == -1)
            {
                this.dead = true;
                this.FieldController.ClickedOnMine(buttonCoordinateX, buttonCoordinateY);
            }

            if (this.Database.GameField[buttonCoordinateX, buttonCoordinateY] == 0)
            {
                this.FieldController.ClickedOnEmpty(buttonCoordinateX, buttonCoordinateY);
            }

            this.clickedButton.Visible = false;
        }
    }
}