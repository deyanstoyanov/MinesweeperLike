namespace MinesweeperLike.App.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class Database : IDatabase
    {
        private int mine = -1;

        private IGameButton[,] buttons;

        private int[,] gameField;

        private Label[,] labels;

        public Database()
        {
            this.Buttons = new IGameButton[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
            this.GameField = new int[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
            this.Labels = new Label[FieldSettings.FieldSizeWidth, FieldSettings.FieldSizeHeight];
        }

        public IGameButton[,] Buttons
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
            this.Buttons[row, col] = button;
        }

        public void AddLabel(Form form)
        {
            int width = this.labels.GetLength(0);
            int height = this.labels.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int buttonLocationX = this.buttons[i, j].LocationX;
                    int buttonLocationY = this.buttons[i, j].LocationY;

                    Label label = CreateLabel(buttonLocationX, buttonLocationY, i, j);
                    form.Controls.Add(label);
                    this.labels[i, j] = label;
                }
            }
        }

        private Label CreateLabel(int buttonLocationX, int buttonLocationY, int i, int j)
        {
            Label label = new Label();
            label.Size = new Size(LabelSettings.LabelSizeWidth, LabelSettings.LabelSizeHeight);
            label.Location = new Point(buttonLocationX, buttonLocationY);
            label.Name = string.Format("{0}-{1}", i, j);
            label.Text = string.Empty;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.BackColor = Color.LightGray;
            label.Font = new Font("Microsoft Sans Serif", LabelSettings.LabelFontSize, FontStyle.Bold, label.Font.Unit);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }

        public void AddMine()
        {
            int width = this.GameField.GetLength(0);
            int height = this.GameField.GetLength(1);
            Random radnom = new Random();

            double minesCount = (width * height) * (MineSettings.PersentOfGameSizeForCreatingMines / 100);

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
            currentLabel.Font = new Font("Microsoft Sans Serif", MineSettings.MineFontSize, FontStyle.Bold, currentLabel.Font.Unit);
            currentLabel.TextAlign = ContentAlignment.MiddleCenter;
            currentLabel.Location = new Point(labelLocationX, labelLocationY);

            return currentLabel;
        }

        public void AddNumber()
        {
            throw new System.NotImplementedException();
        }
    }
}
