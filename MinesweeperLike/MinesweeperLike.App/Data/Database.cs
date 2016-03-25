namespace MinesweeperLike.App.Data
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class Database : IDatabase
    {
        //private int mine = -1;

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

        public void AddLabel(Label label, int row, int col)
        {
            this.Labels[row, col] = label;
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
                this.gameField[mineX, mineY] = -1;
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
