namespace MinesweeperLike.App.Core.ObjectGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;
    using MinesweeperLike.App.Models;

    public class FieldGenerator : IFieldGenerator
    {
        public FieldGenerator(IDatabase database)
        {
            this.Database = database;
            this.ButtonFactory = new ButtonFactory();
            this.LabelFactory = new LabelFactory();
            this.MineFactory = new MineFactory();
        }

        public IDatabase Database { get; }

        public IButtonFactory ButtonFactory { get; }

        public ILabelFactory LabelFactory { get; }

        public IMineFactory MineFactory { get; }

        public int MinesCounter { get; private set; }

        public void CreateLabels(Panel panel, int gameFieldWidth, int gameFieldHeight)
        {
            for (int row = 0; row < gameFieldWidth; row++)
            {
                for (int col = 0; col < gameFieldHeight; col++)
                {
                    int buttonLocationX = this.Database.Buttons[row, col].LocationX;
                    int buttonLocationY = this.Database.Buttons[row, col].LocationY;

                    Label newLabel = this.LabelFactory.CreateLabel(buttonLocationX, buttonLocationY, row, col);
                    panel.Controls.Add(newLabel);
                    this.Database.AddLabel(newLabel, row, col);
                }
            }
        }

        public void CreateButtons(Panel panel, MouseEventHandler eventHandler, int gameFieldWidth, int gameFieldHeight)
        {
            int windowLocationHeight = ButtonSettings.WindowLocationHeight;

            for (int row = 0; row < gameFieldWidth; row++)
            {
                int windowLocationWidth = ButtonSettings.WindowLocationWidth;

                for (int col = 0; col < gameFieldHeight; col++)
                {
                    GameButton newButton =
                        this.ButtonFactory.CreateButton(windowLocationWidth, windowLocationHeight, row, col) as
                        GameButton;
                    if (newButton != null)
                    {
                        newButton.MouseUp += eventHandler;
                        windowLocationWidth = newButton.Right;

                        panel.Controls.Add(newButton);
                        this.Database.AddButton(newButton, row, col);
                    }
                }

                windowLocationHeight += ButtonSettings.ButtonSizeWidth;
            }
        }

        public void CreateMines(int minesCount, int gameFieldWidth, int gameFieldHeight)
        {
            Random random = new Random();

            if (minesCount > gameFieldWidth * gameFieldHeight)
            {
                throw new ArgumentOutOfRangeException("Mines can not be more than game field size");
            }

            List<int> mines = new List<int>(minesCount);

            while (this.MinesCounter != minesCount)
            {
                int mineCoordinateX = random.Next(gameFieldWidth);
                int mineCoordinateY = random.Next(gameFieldHeight);

                string mineCoordinates = $"{mineCoordinateX}{mineCoordinateY}";
                int currentMine = Convert.ToInt32(mineCoordinates);
                var exist = mines.Any(n => n == currentMine);

                if (exist)
                {
                    continue;
                }

                mines.Add(currentMine);

                int mineLocationX = this.Database.Labels[mineCoordinateX, mineCoordinateY].Location.X;
                int mineLocationY = this.Database.Labels[mineCoordinateX, mineCoordinateY].Location.Y;

                this.MineFactory.CreateMine(
                    this.Database,
                    mineCoordinateX,
                    mineCoordinateY,
                    mineLocationX,
                    mineLocationY);

                this.Database.AddMine(mineCoordinateX, mineCoordinateY);
                this.MinesCounter++;
            }
        }

        public void CreateNumbers(int gameFieldWidth, int gameFieldHeight)
        {
            for (int row = 0; row < gameFieldWidth; row++)
            {
                for (int col = 0; col < gameFieldHeight; col++)
                {
                    int currentPosition = this.Database.GameField[row, col];
                    int number = 0;

                    if (currentPosition == MineSettings.Mine)
                    {
                        continue;
                    }

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (this.InBounds(gameFieldWidth, gameFieldHeight, row, i, col, j) && this.NextPositionIsMine(row, i, col, j))
                            {
                                number++;
                            }
                        }
                    }

                    this.Database.AddNumber(number, row, col);
                }
            }
        }

        public void ClearGameField(int gameFieldWidth, int gameFieldHeight)
        {
            this.MinesCounter = 0;

            for (int i = 0; i < gameFieldWidth; i++)
            {
                for (int j = 0; j < gameFieldHeight; j++)
                {
                    if (this.Database.Buttons[i, j].Image != null)
                    {
                        this.Database.Buttons[i, j].Image = null;
                    }

                    if (!this.Database.Buttons[i, j].Visible)
                    {
                        this.Database.Buttons[i, j].Visible = true;
                    }

                    this.Database.Labels[i, j].Text = null;
                    this.Database.Labels[i, j].BackColor = Color.LightGray;
                    this.Database.Labels[i, j].Font = new Font(
                        FieldSettings.Font,
                        LabelSettings.LabelFontSize,
                        FontStyle.Bold,
                        this.Database.Labels[i, j].Font.Unit);
                    this.Database.Labels[i, j].ForeColor = Color.Black;
                    this.Database.GameField[i, j] = 0;
                    this.Database.MarketButtons[i, j] = false;
                }
            }
        }

        private bool InBounds(int width, int height, int row, int k, int col, int l)
        {
            return row + k >= 0 && col + l >= 0 && row + k < width && col + l < height;
        }

        private bool NextPositionIsMine(int row, int k, int col, int l)
        {
            return this.Database.GameField[row + k, col + l] == MineSettings.Mine;
        }
    }
}