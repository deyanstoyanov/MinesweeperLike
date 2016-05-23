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

        private bool start;

        private bool dead;

        private bool win;

        public GameController(Form form, int gameFieldWidth, int gameFieldHeight)
        {
            this.Form = form;
            this.Database = new Database(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator = new FieldGenerator(this.Database);
            this.FormGenerator = new FormGenerator(this.Database, this.Form);
            this.FieldController = new FieldController(this.Database, this.FieldGenerator);
            this.clickedButton = new GameButton();
            this.Timer = new Timer();
        }

        public IDatabase Database { get; }

        public IFieldGenerator FieldGenerator { get; }

        public IFormGenerator FormGenerator { get; }

        public IFieldController FieldController { get; }

        public int MarketButtonsCounter { get; set; }

        public Form Form { get; }

        public Timer Timer { get; private set; }

        public Panel Panel { get; }

        public int Time { get; private set; }

        public void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.dead || this.win)
            {
                return;
            }

            this.start = true;
            this.clickedButton = sender as GameButton;

            if (this.clickedButton.Text != string.Empty)
            {
                this.clickedButton.Text = string.Empty;
                this.MarketButtonsCounter--;
                this.UpdateMarketButtonsCounter(this.MarketButtonsCounter);
                return;
            }

            this.clickedButton.Text = FieldSettings.FlagChar;
            this.clickedButton.ForeColor = Color.Red;
            this.clickedButton.Font = new Font(FieldSettings.Font, 8);
            this.clickedButton.TextAlign = ContentAlignment.MiddleCenter;
            this.MarketButtonsCounter++;
            this.UpdateMarketButtonsCounter(this.MarketButtonsCounter);
        }

       

        public void LeftButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            this.clickedButton = sender as GameButton;

            if (this.dead || this.win || this.clickedButton.Text != string.Empty)
            {
                return;
            }

            this.start = true;
            this.Timer.Start();

            int buttonCoordinateX = this.clickedButton.Row;
            int buttonCoordinateY = this.clickedButton.Col;

            if (this.Database.GameField[buttonCoordinateX, buttonCoordinateY] == -1)
            {
                this.start = false;
                this.Timer.Stop();
                this.dead = true;
                this.FieldController.ClickedOnMine(buttonCoordinateX, buttonCoordinateY);
                this.YouDead();
            }

            if (this.Database.GameField[buttonCoordinateX, buttonCoordinateY] == 0)
            {
                this.FieldController.ClickedOnEmpty(buttonCoordinateX, buttonCoordinateY);
                this.UpdateMarketButtonsCounter(this.MarketButtonsCounter);
            }

            this.clickedButton.Visible = false;
            this.Database.MarketButtons[buttonCoordinateX, buttonCoordinateY] = true;

            var marketButtons = this.FieldController.GetMarketButtons();
            bool allMarket = marketButtons.All(n => n);

            if (allMarket)
            {
                this.win = true;
                this.start = false;
                this.Timer.Stop();
                this.FieldController.MarkAllMinesWithFlag();
                this.YouWin();
            }
        }

        public void IncreaseTIme(object sender, EventArgs e)
        {
            if (!this.FormGenerator.StatusStrip.Items.Contains(this.FormGenerator.TimerStatusLabel))
            {
                this.FormGenerator.StatusStrip.Items.Add(this.FormGenerator.TimerStatusLabel);
            }

            if (this.start)
            {
                this.Time++;
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(this.Time);

            this.FormGenerator.TimerStatusLabel.Text =
                $"Time:[{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}]";
        }

        public void UpdateMarketButtonsCounter(int marketButtonsCount)
        {
            this.FormGenerator.MarketButtonsStauStatusLabel.Text =
               $"Market: {marketButtonsCount} / {this.FieldGenerator.MinesCounter} mines";
            this.FormGenerator.StatusStrip.Items.Add(this.FormGenerator.MarketButtonsStauStatusLabel);
        }

        public void LoadMarketButtonsCounter(int minesCunter)
        {
            this.FormGenerator.MarketButtonsStauStatusLabel.Text = $"Market: 0 / {minesCunter} mines";
            this.FormGenerator.StatusStrip.Items.Add(this.FormGenerator.MarketButtonsStauStatusLabel);
        }

        private void YouDead()
        {
            this.FormGenerator.MarketButtonsStauStatusLabel.Text = @"DEAD!";
        }

        private void YouWin()
        {
            this.FormGenerator.MarketButtonsStauStatusLabel.Text = @"COMPLETED!";
        }
    }
}