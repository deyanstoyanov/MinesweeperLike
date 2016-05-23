﻿namespace MinesweeperLike.App.Core
{
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
            this.SetGameField(
                this.form, 
                this.gameController, 
                this.minesCount, 
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
    }
}