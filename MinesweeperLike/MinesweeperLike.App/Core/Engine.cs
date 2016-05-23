namespace MinesweeperLike.App.Core
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class Engine : IEngine
    {
        private readonly Form form;

        private readonly IGameController gameController;

        private readonly int gameFieldHeight = 9;

        private readonly int gameFieldWidth = 9;

        private readonly int minesCount = 10;

        public Engine(Form form)
        {
            this.form = form;
            this.gameController = new GameController(this.form, this.gameFieldWidth, this.gameFieldHeight);
        }

        public void Run()
        {
            this.SetGameForm(this.form, this.gameController);
            this.SetGameField(
                this.form, 
                this.gameController, 
                this.minesCount, 
                this.gameFieldWidth, 
                this.gameFieldHeight);
        }

        private void SetGameForm(Form form, IGameController gameController)
        {
            this.gameController.FieldController.TimerConfiguration(
                this.gameController.Timer,
                this.gameController.IncreaseTIme);
            this.gameController.GameFormGenerator.CreateMenu(form, this.MenuStripItemsEvents);
            this.gameController.GameFormGenerator.LoadStatusBar();
            this.gameController.GameFormGenerator.FormSize(
                form, 
                gameController.FieldGenerator.GameField, 
                this.gameFieldWidth, 
                this.gameFieldHeight);
        }

        private void SetGameField(
            Form form, 
            IGameController gameController, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight)
        {
            this.gameController.FieldController.CreateGameField(
                form, 
                gameController.FieldGenerator.GameField, 
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
                    
                    break;
                case "Restart":
                    
                    break;
                case "Exit":
                    this.ExitGame();
                    break;
            }
        }

        private void ExitGame()
        {
            Environment.Exit(1);
        }
    }
}