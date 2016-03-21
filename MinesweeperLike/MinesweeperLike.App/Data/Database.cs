namespace MinesweeperLike.App.Data
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class Database : IDatabase
    {

        private Button[,] buttons;

        private int[,] gameField;

        private Label[,] labels;

        private IEnumerable<IMine> mines;

        public Database(Button[,] buttons, int[,] gameField, Label[,] labels)
        {
            this.Buttons = buttons;
            this.GameField = gameField;
            this.Labels = labels;
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
            int windowPositionX = 10;
            int windowPositionY = 60;

            for (int i = 0; i < this.Buttons.GetLength(0); i++)
            {
                windowPositionX = 10;

                for (int j = 0; j < this.Buttons.GetLength(1); j++)
                {
                    this.Buttons[i, j] = this.CreateNewButton(i, j, windowPositionX, windowPositionY);
                    windowPositionX = this.Buttons[i, j].Right;
                }

                windowPositionY += 30;
            }
        }

        private Button CreateNewButton(int i, int j, int windowPositionX, int windowPositionY)
        {
            var button = new Button();
            button.Location = new Point(windowPositionX, windowPositionY);
            button.Size = new Size(30, 30);
            button.Font = new Font("Microsoft Sans Serif", 5);
            button.Name = string.Format("{0}-{1}", i, j);

            return button;
        }

        public void AddLabels(Form form)
        {
            throw new System.NotImplementedException();
        }

        public void AddMines()
        {
            throw new System.NotImplementedException();
        }

        public void AddNumbers()
        {
            throw new System.NotImplementedException();
        }
    }
}
