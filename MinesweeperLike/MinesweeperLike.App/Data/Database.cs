namespace MinesweeperLike.App.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Models;

    public class Database : IDatabase
    {
        private GameButton[,] buttons;

        private int[,] gameField;

        private Label[,] labels;

        public Database()
        {
            this.Buttons = new GameButton[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
            this.GameField = new int[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
            this.Labels = new Label[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
        }

        public GameButton[,] Buttons
        {
            get
            {
                return this.buttons;
            }

            private set
            {
                this.buttons = value;
            }
        }

        public int[,] GameField
        {
            get
            {
                return this.gameField;
            }

            private set
            {
                this.gameField = value;
            }
        }

        public Label[,] Labels
        {
            get
            {
                return this.labels;
            }

            private set
            {
                this.labels = value;
            }
        }

        public void AddButton(IGameButton button, int row, int col)
        {
            this.Buttons[row, col] = button as GameButton;
        }

        public void AddLabel(Label label, int row, int col)
        {
            this.Labels[row, col] = label;
        }

        public void AddMine(int row, int col)
        {
            this.GameField[row, col] = -1;
        }

        public void AddNumber()
        {
            throw new NotImplementedException();
        }
    }
}
