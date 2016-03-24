namespace MinesweeperLike.App.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class Database : IDatabase
    {
        private const int ButtonAndLabelSize = 22;

        private const int GameFieldSizeWidth = 16;

        private const int GameFieldSizeHeight = 16;

        private const int PersentOfGameSizeForCreatingMines = 20;

        private const int MineFontSize = 22;

        private const int LabelFontSize = 12;

        private int windowPositionX = 10;

        private int windowPositionY = 60;

        private int mine = -1;

        private Button[,] buttons;

        private int[,] gameField;

        private Label[,] labels;

        private IEnumerable<IMine> mines;

        public Database()
        {
            this.Buttons = new Button[GameFieldSizeWidth, GameFieldSizeHeight];
            this.GameField = new int[GameFieldSizeWidth, GameFieldSizeHeight];
            this.Labels = new Label[GameFieldSizeWidth, GameFieldSizeHeight];
            this.mines = new List<IMine>();
        }

        public Button[,] Buttons
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

        public IEnumerable<IMine> Mines
        {
            get
            {
                return this.mines;
            }

            private set
            {
                this.mines = value;
            }
        }

        public void AddButtons()
        {
            int width = this.Buttons.GetLength(0);
            int height = this.Buttons.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                this.windowPositionX = 10;

                for (int j = 0; j < height; j++)
                {
                    this.Buttons[i, j] = this.CreateNewButton(i, j, this.windowPositionX, this.windowPositionY);
                    this.windowPositionX = this.Buttons[i, j].Right;
                }

                this.windowPositionY += ButtonAndLabelSize;
            }
        }

        private Button CreateNewButton(int i, int j, int windowPositionX, int windowPositionY)
        {
            Button button = new Button();
            button.Location = new Point(windowPositionX, windowPositionY);
            button.Size = new Size(ButtonAndLabelSize, ButtonAndLabelSize);
            button.FlatStyle = FlatStyle.Popup;
            button.FlatAppearance.MouseOverBackColor = button.ForeColor;
            button.Name = string.Format("{0}-{1}", i, j);

            return button;
        }

        public void AddLabels(Form form)
        {
            int width = this.labels.GetLength(0);
            int height = this.labels.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int buttonLocationX = this.buttons[i, j].Location.X;
                    int buttonLocationY = this.buttons[i, j].Location.Y;

                    Label label = CreateLabel(buttonLocationX, buttonLocationY, i, j);
                    form.Controls.Add(label);
                    this.labels[i, j] = label;
                }
            }
        }

        private Label CreateLabel(int buttonLocationX, int buttonLocationY, int i, int j)
        {
            Label label = new Label();
            label.Size = new Size(ButtonAndLabelSize, ButtonAndLabelSize);
            label.Location = new Point(buttonLocationX, buttonLocationY);
            label.Name = string.Format("{0}-{1}", i, j);
            label.Text = string.Empty;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.BackColor = Color.LightGray;
            label.Font = new Font("Microsoft Sans Serif", LabelFontSize, FontStyle.Bold, label.Font.Unit);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }

        public void AddMines()
        {
            int width = this.GameField.GetLength(0);
            int height = this.GameField.GetLength(1);
            Random radnom = new Random();

            double minesCount = (width * height) * (PersentOfGameSizeForCreatingMines / 100);

            for (int i = 0; i < (int)minesCount; i++)
            {
                int mineX = radnom.Next(width);
                int mineY = radnom.Next(height);

                var currentLabel = this.CreateMine(mineX, mineY);
                this.labels[mineX, mineY] = currentLabel;
                this.gameField[mineX, mineY] = this.mine;
            }
        }

        private Label CreateMine(int mineX, int mineY)
        {
            Label currentLabel = this.labels[mineX, mineY];
            int labelLocationX = this.labels[mineX, mineY].Location.X;
            int labelLocationY = this.labels[mineX, mineY].Location.Y;

            currentLabel.Text = "*";
            currentLabel.Font = new Font("Microsoft Sans Serif", MineFontSize, FontStyle.Bold, currentLabel.Font.Unit);
            currentLabel.TextAlign = ContentAlignment.MiddleCenter;
            currentLabel.Location = new Point(labelLocationX, labelLocationY);

            return currentLabel;
        }

        public void AddNumbers()
        {
            throw new System.NotImplementedException();
        }
    }
}
