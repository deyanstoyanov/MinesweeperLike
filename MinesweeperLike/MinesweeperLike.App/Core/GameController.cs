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
    using MinesweeperLike.App.Models;

    public class GameController : IGameController
    {
        private bool dead;

        public GameController(IDatabase database, Form gameField)
        {
            this.Database = database;
            this.GameField = gameField;
            this.ButtonFactory = new ButtonFactory();
            this.LabelFactory = new LabelFactory();
            this.MineFactory = new MineFactory();
            this.ClickedButton = new GameButton();
            this.LabelToShow = new Label();

            // this.Time = time;
            this.Timer = new Timer();
        }

        public IButtonFactory ButtonFactory { get; }

        public ILabelFactory LabelFactory { get; }

        public IMineFactory MineFactory { get; }

        public int MineValue
        {
            get
            {
                return -1;
            }
        }

        public IDatabase Database { get; private set; }

        public GameButton ClickedButton { get; private set; }

        public Form GameField { get; private set; }

        public Label LabelToShow { get; private set; }

        public Timer Timer { get; private set; }

        public int Time { get; private set; }

        public void CreateButtons(Form form, MouseEventHandler mouseClick)
        {
            int width = this.Database.Buttons.GetLength(0);
            int height = this.Database.Buttons.GetLength(1);

            int windowLocationHeight = ButtonSettings.WindowLocationHeight;

            for (int row = 0; row < width; row++)
            {
                int windowLocationWidth = ButtonSettings.WindowLocationWidth;

                for (int col = 0; col < height; col++)
                {
                    GameButton newButton =
                        this.ButtonFactory.CreateButton(windowLocationWidth, windowLocationHeight, row, col) as
                        GameButton;
                    if (newButton != null)
                    {
                        newButton.MouseUp += mouseClick;
                        windowLocationWidth = newButton.Right;

                        form.Controls.Add(newButton);
                        this.Database.AddButton(newButton, row, col);
                    }
                }

                windowLocationHeight += ButtonSettings.ButtonSizeWidth;
            }
        }

        public void CreateLabels(Form form)
        {
            int width = this.Database.Labels.GetLength(0);
            int height = this.Database.Labels.GetLength(1);

            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    int buttonLocationX = this.Database.Buttons[row, col].Location.X;
                    int buttonLocationY = this.Database.Buttons[row, col].Location.Y;

                    Label newLabel = this.LabelFactory.CreateLabel(buttonLocationX, buttonLocationY, row, col);
                    form.Controls.Add(newLabel);
                    this.Database.AddLabel(newLabel, row, col);
                }
            }
        }

        public void CreateMines()
        {
            Random random = new Random();
            int width = this.Database.GameField.GetLength(0);
            int height = this.Database.GameField.GetLength(1);

            // int minesCount = (int)((width * height) * (MineSettings.PersentOfGameSizeForCreatingMines / 100));
            int minesCount = MineSettings.MineCount;

            if (minesCount > width * height)
            {
                throw new ArgumentOutOfRangeException("Mines can not be more than game field size");
            }

            int minesCounter = 0;
            List<int> mines = new List<int>(minesCount);

            while (minesCounter != minesCount)
            {
                int mineCoordinateX = random.Next(width);
                int mineCoordinateY = random.Next(height);

                string mineCoordinates = string.Format("{0}{1}", mineCoordinateX, mineCoordinateY);
                int currentMine = Convert.ToInt32(mineCoordinates);
                bool exist = mines.Any(n => n == currentMine);

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
                minesCounter++;
            }
        }

        public void CreateNumbers()
        {
            int width = this.Database.GameField.GetLength(0);
            int height = this.Database.GameField.GetLength(1);

            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    int currentPosition = this.Database.GameField[row, col];
                    int number = 0;

                    if (currentPosition == this.MineValue)
                    {
                        continue;
                    }

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (this.InBounds(width, height, row, i, col, j) && this.NextPositionIsMine(row, i, col, j))
                            {
                                number++;
                            }
                        }
                    }

                    this.Database.AddNumber(number, row, col);
                }
            }
        }

        public void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.dead)
            {
                return;
            }

            this.ClickedButton = sender as GameButton;

            if (this.ClickedButton.Text != string.Empty)
            {
                this.ClickedButton.Text = string.Empty;
                return;
            }

            this.ClickedButton.Text = FieldSettings.FlagChar;
            this.ClickedButton.ForeColor = Color.Red;
            this.ClickedButton.Font = new Font(FieldSettings.Font, 8);
            this.ClickedButton.TextAlign = ContentAlignment.MiddleCenter;
        }

        public void LeftButtonOnClick(object sender, EventArgs e)
        {
            this.ClickedButton = sender as GameButton;

            if (this.dead || this.ClickedButton.Text != string.Empty)
            {
                return;
            }

            int buttonCoordinateX = this.ClickedButton.Row;
            int buttonCoordinateY = this.ClickedButton.Col;

            this.LabelToShow = this.Database.Labels[buttonCoordinateX, buttonCoordinateY];

            if (this.IsMine(buttonCoordinateX, buttonCoordinateY))
            {
                this.ClickedOnMine();
            }

            if (this.IsEmpty(buttonCoordinateX, buttonCoordinateY))
            {
                this.RemoveEmptyButtons(buttonCoordinateX, buttonCoordinateY);
            }

            this.ClickedButton.Visible = false;
        }

        private void ClickedOnMine()
        {
            this.LabelToShow.ForeColor = Color.Black;
            this.LabelToShow.BackColor = Color.Red;

            this.dead = true;
            int width = this.Database.Buttons.GetLength(0);
            int height = this.Database.Buttons.GetLength(1);
            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    if (this.IsMine(row, col))
                    {
                        this.Database.Buttons[row, col].Visible = false;
                    }
                }
            }
        }

        private bool IsMine(int buttonCoordinateX, int buttonCoordinateY)
        {
            return this.Database.GameField[buttonCoordinateX, buttonCoordinateY] == this.MineValue;
        }

        private bool IsEmpty(int buttonX, int buttonY)
        {
            return this.Database.GameField[buttonX, buttonY] == 0;
        }

        private bool InBounds(int width, int height, int row, int k, int col, int l)
        {
            return row + k >= 0 && col + l >= 0 && row + k < width && col + l < height;
        }

        private bool NextPositionIsMine(int row, int k, int col, int l)
        {
            return this.Database.GameField[row + k, col + l] == this.MineValue;
        }

        private void RemoveEmptyButtons(int buttonX, int buttonY)
        {
            if (!this.Database.Buttons[buttonX, buttonY].Visible)
            {
                return;
            }

            int width = this.Database.GameField.GetLength(0);
            int height = this.Database.GameField.GetLength(1);

            var currentButton = this.Database.Buttons[buttonX, buttonY];
            int currentPosition = this.Database.GameField[buttonX, buttonY];

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!this.InBounds(width, height, buttonX, i, buttonY, j))
                    {
                        continue;
                    }

                    currentButton.Visible = false;
                    if (currentPosition == 0)
                    {
                        this.RemoveEmptyButtons(buttonX + i, buttonY + j);
                    }
                }
            }
        }
    }
}