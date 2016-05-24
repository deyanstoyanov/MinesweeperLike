namespace MinesweeperLike.App.Core
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.ObjectGenerators;
    using MinesweeperLike.App.Data;
    using MinesweeperLike.App.Models;

    public class GameController : IGameController
    {
        private readonly IDatabase database;

        private readonly Form form;

        private GameButton clickedButton;

        private int time;

        private bool dead;

        private bool start;

        private bool win;

        public GameController(Form form, int gameFieldWidth, int gameFieldHeight)
        {
            this.form = form;
            this.database = new Database(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator = new FieldGenerator(this.database);
            this.FormGenerator = new FormGenerator(this.database, this.FieldGenerator, this.form);
            this.FieldController = new FieldController(this.database, this.FieldGenerator);
            this.clickedButton = new GameButton();
            this.Timer = new Timer();
        }

        public IFieldGenerator FieldGenerator { get; }

        public IFormGenerator FormGenerator { get; }

        public IFieldController FieldController { get; }

        public Timer Timer { get; }

        public void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.dead || this.win)
            {
                return;
            }

            this.clickedButton = sender as GameButton;

            if (this.clickedButton.Text != string.Empty)
            {
                this.clickedButton.Text = string.Empty;
                this.FieldController.MarketButtonsCounter--;
                this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
                return;
            }

            this.clickedButton.Text = FieldSettings.FlagChar;
            this.clickedButton.ForeColor = Color.Red;
            this.clickedButton.Font = new Font(FieldSettings.Font, 8);
            this.clickedButton.TextAlign = ContentAlignment.MiddleCenter;
            this.FieldController.MarketButtonsCounter++;
            this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
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

            if (this.database.GameField[buttonCoordinateX, buttonCoordinateY] == -1)
            {
                this.start = false;
                this.Timer.Stop();
                this.dead = true;
                this.FieldController.ClickedOnMine(buttonCoordinateX, buttonCoordinateY);
                this.YouDead();
            }

            if (this.database.GameField[buttonCoordinateX, buttonCoordinateY] == 0)
            {
                this.FieldController.ClickedOnEmpty(buttonCoordinateX, buttonCoordinateY);
                this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
            }

            this.clickedButton.Visible = false;
            this.database.MarketButtons[buttonCoordinateX, buttonCoordinateY] = true;

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
                this.time++;
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(this.time);

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

        public void RestartGame(object sender, EventArgs e)
        {
            this.start = false;
            this.dead = false;
            this.win = false;
            this.time = 0;
            this.FieldController.MarketButtonsCounter = 0;
            this.FieldController.RestartGameField();
            this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
            this.FormGenerator.TimerStatusLabel.Text = @"Time:[00:00:00]";
        }

        public void CreateNewGame(
            Form form, 
            MouseEventHandler eventHandler, 
            int gameFieldWidth, 
            int gameFieldHeight, 
            int minesCount)
        {
            this.start = false;
            this.dead = false;
            this.win = false;
            this.time = 0;
            this.FieldController.MarketButtonsCounter = 0;

            this.FieldGenerator.ClearGameField(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateMines(minesCount, gameFieldWidth, gameFieldHeight);
            this.FieldGenerator.CreateNumbers(gameFieldWidth, gameFieldHeight);
            this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
            this.FormGenerator.TimerStatusLabel.Text = @"Time:[00:00:00]";
        }

        public void CreateNewGameWithOtherType(
            Form form, 
            IGameController gameController, 
            MouseEventHandler eventHandler, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight)
        {
            gameController.FieldController.TimerConfiguration(gameController.Timer, gameController.IncreaseTIme);
            gameController.FormGenerator.FormSize(form, gameFieldWidth, gameFieldHeight);
            gameController.FormGenerator.LoadStatusBar();
            gameController.UpdateMarketButtonsCounter(gameController.FieldController.MarketButtonsCounter);
            gameController.LoadMarketButtonsCounter(minesCount);
            gameController.FormGenerator.CreateGameField(
                form, 
                eventHandler, 
                minesCount, 
                gameFieldWidth, 
                gameFieldHeight);
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