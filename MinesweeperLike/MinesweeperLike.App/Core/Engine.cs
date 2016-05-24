namespace MinesweeperLike.App.Core
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class Engine : IEngine
    {
        private readonly Form form;

        private IGameController gameController;

        private int gameFieldHeight = 9;

        private int gameFieldWidth = 9;

        private int minesCount = 10;

        public Engine(Form form)
        {
            this.form = form;
            this.gameController = new GameController(this.form, this.gameFieldWidth, this.gameFieldHeight);
        }

        public void Run()
        {
            this.SetGameForm(this.form, this.minesCount);
            this.SetGameField(
                this.form, 
                this.minesCount, 
                this.gameFieldWidth, 
                this.gameFieldHeight);
        }

        private void SetGameForm(Form form, int minesCount)
        {
            this.gameController.FieldController.TimerConfiguration(
                this.gameController.Timer,
                this.gameController.IncreaseTIme);
            this.gameController.FormGenerator.CreateMenu(form, this.MenuStripItemsEvents);
            this.gameController.FormGenerator.LoadStatusBar();
            this.gameController.LoadMarketButtonsCounter(minesCount);
            this.gameController.FormGenerator.FormSize(
                form, 
                this.gameFieldWidth, 
                this.gameFieldHeight);
        }

        private void SetGameField(
            Form form, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight)
        {
            this.gameController.FormGenerator.CreateGameField(
                form, 
                this.MouseClick, 
                minesCount, 
                gameFieldWidth, 
                gameFieldHeight);
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.gameController.LeftButtonOnClick(sender, e);
                    break;
                case MouseButtons.Right:
                    this.gameController.RightButtonOnClick(sender, e);
                    break;
            }
        }

        private void MenuStripItemsEvents(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            switch (item.Text)
            {
                case "New Game":
                    this.NewGame(sender, e, this.minesCount, this.gameFieldWidth, this.gameFieldHeight);
                    break;
                case "Restart":
                    this.gameController.RestartGame(sender, e);
                    break;
                case "Exit":
                    this.ExitGame();
                    break;
                case "9x9, 10 mines":
                    this.NewGame(sender, e, this.minesCount = 10, this.gameFieldWidth = 9, this.gameFieldHeight = 9);
                    break;
                case "9x9, 35 mines":
                    this.NewGame(sender, e, this.minesCount = 35, this.gameFieldWidth = 9, this.gameFieldHeight = 9);
                    break;
                case "16x16, 40 mines":
                    this.NewGame(sender, e, this.minesCount = 40, this.gameFieldWidth = 16, this.gameFieldHeight = 16);
                    break;
                case "16x16, 99 mines":
                    this.NewGame(sender, e, this.minesCount = 99, this.gameFieldWidth = 16, this.gameFieldHeight = 16);
                    break;
                case "30x16, 130 mines":
                    this.NewGame(sender, e, this.minesCount = 130, this.gameFieldWidth = 16, this.gameFieldHeight = 30);
                    break;
                case "30x16, 170 mines":
                    this.NewGame(sender, e, this.minesCount = 170, this.gameFieldWidth = 16, this.gameFieldHeight = 30);
                    break;
            }
        }

        private void NewGame(object sender, EventArgs eventArgs, int minesCount, int gameFieldWidth,int gameFieldHeight)
        {
            int currentGameFieldWidth = this.gameController.FieldGenerator.Database.Buttons.GetLength(0);
            int currentgameFieldHeight = this.gameController.FieldGenerator.Database.Buttons.GetLength(1);

            if (gameFieldWidth > currentGameFieldWidth || gameFieldHeight > currentgameFieldHeight
                || gameFieldWidth < currentGameFieldWidth || gameFieldHeight < currentgameFieldHeight)
            {
                this.form.Controls.Remove(this.gameController.FormGenerator.StatusStrip);
                this.form.Controls.Remove(this.gameController.FormGenerator.GameField);

                this.gameController.FieldGenerator.ClearGameField(
                    this.gameController.FieldGenerator.Database.Buttons.GetLength(0),
                    this.gameController.FieldGenerator.Database.Buttons.GetLength(1));

                this.gameController = new GameController(this.form, gameFieldWidth, gameFieldHeight);
                this.gameController.CreateNewGameWithOtherType(this.form, this.gameController, this.MouseClick, minesCount, gameFieldWidth, gameFieldHeight);
                //this.NewGameWithOtherType(minesCount, fieldWidth, fieldHeight);
            }
            else
            {
                this.gameController.CreateNewGame(this.form, this.MouseClick, gameFieldWidth, gameFieldHeight, minesCount);
            }
        }

        private void ExitGame()
        {
            Environment.Exit(1);
        }
    }
}