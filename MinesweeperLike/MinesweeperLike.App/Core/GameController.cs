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

        private bool solvedByPowerOfCSharp;

        public GameController(Form form, int gameFieldWidth, int gameFieldHeight)
        {
            this.form = form;
            this.database = new Database(gameFieldWidth, gameFieldHeight);
            this.FieldGenerator = new FieldGenerator(this.database);
            this.FormGenerator = new FormGenerator(this.form, this.database, this.FieldGenerator);
            this.FieldController = new FieldController(this.database);
            this.clickedButton = new GameButton();
            this.Timer = new Timer();
        }

        public IFieldGenerator FieldGenerator { get; }

        public IFormGenerator FormGenerator { get; }

        public IFieldController FieldController { get; }

        public Timer Timer { get; }

        public void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.dead || this.win || this.solvedByPowerOfCSharp)
            {
                return;
            }

            this.clickedButton = sender as GameButton;

            if (this.clickedButton.Image != null)
            {
                this.clickedButton.Image = null;
                this.FieldController.MarketButtonsCounter--;
                this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
                return;
            }

            this.clickedButton.Image = this.FieldController.Flag;
            this.FieldController.MarketButtonsCounter++;
            this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
        }

        public void LeftButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            this.clickedButton = sender as GameButton;

            if (this.dead || this.win || this.clickedButton.Image != null)
            {
                return;
            }

            this.start = true;
            this.solvedByPowerOfCSharp = false;
            this.Timer.Start();

            int buttonCoordinateX = this.clickedButton.Row;
            int buttonCoordinateY = this.clickedButton.Col;

            if (this.database.GameField[buttonCoordinateX, buttonCoordinateY] == MineSettings.Mine)
            {
                this.start = false;
                this.dead = true;
                this.Timer.Stop();
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
                $"Marked: {marketButtonsCount} / {this.FieldGenerator.MinesCounter} mines";
            this.FormGenerator.StatusStrip.Items.Add(this.FormGenerator.MarketButtonsStauStatusLabel);
        }

        public void LoadMarketButtonsCounter(int minesCunter)
        {
            this.FormGenerator.MarketButtonsStauStatusLabel.Text = $"Marked: 0 / {minesCunter} mines";
            this.FormGenerator.StatusStrip.Items.Add(this.FormGenerator.MarketButtonsStauStatusLabel);
        }

        public void RestartGame(object sender, EventArgs e, int gameFieldWidth, int gameFieldHeight)
        {
            this.solvedByPowerOfCSharp = false;
            this.start = false;
            this.dead = false;
            this.win = false;
            this.time = 0;
            this.FieldController.MarketButtonsCounter = 0;
            this.FieldController.RestartGameField(gameFieldWidth, gameFieldHeight);
            this.UpdateMarketButtonsCounter(this.FieldController.MarketButtonsCounter);
            this.FormGenerator.TimerStatusLabel.Text = @"Time:[00:00:00]";
        }

        public void SolveGame(int gameFieldWidth, int gameFieldHeight)
        {
            if (this.dead || this.win)
            {
                return;
            }

            if (!this.start)
            {
                MessageBox.Show(@"Game has not been started yet!", @"Unable to solve!",
    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            this.FieldController.SolveGameField(gameFieldWidth, gameFieldHeight);
            this.UpdateMarketButtonsCounter(this.FieldGenerator.MinesCounter);
            this.solvedByPowerOfCSharp = true;
            this.Timer.Stop();
        }

        public void CreateNewGame(
            Form form,
            MouseEventHandler eventHandler,
            int minesCount,
            int gameFieldWidth,
            int gameFieldHeight)
        {
            this.solvedByPowerOfCSharp = false;
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