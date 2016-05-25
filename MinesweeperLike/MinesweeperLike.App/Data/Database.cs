namespace MinesweeperLike.App.Data
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Models;

    public class Database : IDatabase
    {
        public Database(int gameFieldWidth, int gameFieldHeight)
        {
            this.Buttons = new GameButton[gameFieldWidth, gameFieldHeight];
            this.GameField = new int[gameFieldWidth, gameFieldHeight];
            this.Labels = new Label[gameFieldWidth, gameFieldHeight];
            this.MarketButtons = new bool[gameFieldWidth, gameFieldHeight];
        }

        public GameButton[,] Buttons { get; private set; }

        public bool[,] MarketButtons { get; }

        public int[,] GameField { get; private set; }

        public Label[,] Labels { get; private set; }

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
            this.GameField[row, col] = MineSettings.Mine;
            this.MarketButtons[row, col] = true;
        }

        public void AddNumber(int number, int row, int col)
        {
            this.GameField[row, col] = number;
            Color newColor = this.SetTheColorOfTheNumber(number);
            this.Labels[row, col].ForeColor = newColor;
            this.Labels[row, col].Text = number.ToString();

            if (number == 0)
            {
                this.Labels[row, col].Text = string.Empty;
            }
        }

        private Color SetTheColorOfTheNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return Color.Empty;
                case 1:
                    return Color.FromArgb(0, 0, 255);
                case 2:
                    return Color.FromArgb(0, 127, 0);
                case 3:
                    return Color.FromArgb(255, 0, 0);
                case 4:
                    return Color.FromArgb(0, 0, 127);
                case 5:
                    return Color.FromArgb(127, 0, 0);
                case 6:
                    return Color.FromArgb(0, 127, 127);
                case 7:
                    return Color.FromArgb(0, 0, 0);
                case 8:
                    return Color.FromArgb(127, 127, 127);
                default:
                    throw new ArgumentOutOfRangeException(nameof(number), @"Number should be in range [1-8]");
            }
        }
    }
}