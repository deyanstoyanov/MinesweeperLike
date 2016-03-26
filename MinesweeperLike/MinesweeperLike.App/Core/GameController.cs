namespace MinesweeperLike.App.Core
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;
    using MinesweeperLike.App.Models;

    using sweeperLike.App.Constants;

    public class GameController : IGameController
    {
        private readonly IButtonFactory buttonFactory;

        private readonly ILabelFactory labelFactory;

        private readonly IMineFactory mineFactory;

        private IDatabase database;

        private GameButton clickedButton;

        private Label labelToShow;

        private Timer timer;

        private int time;

        private Form gameField;

        private bool dead;


        public GameController(IDatabase database, Form gameField)
        {
            this.Database = database;
            this.GameField = gameField;
            this.buttonFactory = new ButtonFactory();
            this.labelFactory = new LabelFactory();
            this.mineFactory = new MineFactory();
            this.ClickedButton = new GameButton();
            this.LabelToShow = new Label();
            this.Time = time;
            this.timer = new Timer();
        }

        public IButtonFactory ButtonFactory
        {
            get
            {
                return this.buttonFactory;
            }
        }

        public ILabelFactory LabelFactory
        {
            get
            {
                return this.labelFactory;
            }
        }

        public IMineFactory MineFactory
        {
            get
            {
                return this.mineFactory;
            }
        }

        public IDatabase Database
        {
            get
            {
                return this.database;
            }

            private set
            {
                this.database = value;
            }
        }

        public int MineValue
        {
            get
            {
                return -1;
            }
        }

        public GameButton ClickedButton
        {
            get
            {
                return this.clickedButton;
            }

            private set
            {
                this.clickedButton = value;
            }
        }

        public Form GameField
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

        public Label LabelToShow
        {
            get
            {
                return this.labelToShow;
            }

            private set
            {
                this.labelToShow = value;
            }
        }

        public Timer Timer
        {
            get
            {
                return this.timer;
            }

            private set
            {
                this.timer = value;
            }
        }

        public int Time
        {
            get
            {
                return this.time;
            }

            private set
            {
                this.time = value;
            }
        }

        public void MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.LeftButtonOnClick(sender, e);
                    break;
            }
        }

        public void CreateButtons(Form form)
        {
            int width = this.Database.Buttons.GetLength(0);
            int height = this.Database.Buttons.GetLength(1);

            int windowLocationHeight = ButtonSettings.WindowLocationHeight;

            for (int row = 0; row < width; row++)
            {
                int windowLocationWidth = ButtonSettings.WindowLocationWidth;

                for (int col = 0; col < height; col++)
                {
                    GameButton newButton = this.ButtonFactory.CreateButton(windowLocationWidth, windowLocationHeight, row, col) as GameButton;
                    if (newButton != null)
                    {
                        newButton.MouseUp += this.MouseClick;
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
                    int buttonLocationX = this.Database.Buttons[row, col].LocationX;
                    int buttonLocationY = this.Database.Buttons[row, col].LocationY;

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

            int minesCount = (int)((width * height) * (MineSettings.PersentOfGameSizeForCreatingMines / 100));

            for (int i = 0; i < minesCount; i++)
            {
                int mineCoordinateX = random.Next(width);
                int mineCoordinateY = random.Next(height);
                int mineLocationX = this.Database.Labels[mineCoordinateX, mineCoordinateY].Location.X;
                int mineLocationY = this.Database.Labels[mineCoordinateX, mineCoordinateY].Location.Y;

                this.MineFactory.CreateMine(
                    this.Database,
                    mineCoordinateX,
                    mineCoordinateY,
                    mineLocationX,
                    mineLocationY);

                this.Database.AddMine(mineCoordinateX, mineCoordinateY);
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
                            if (this.InBounds(width, height, row, i, col, j) && this.IsMine(row, i, col, j))
                            {
                                number++;                                
                            }
                        }
                    }

                    this.Database.AddNumber(number, row, col);
                }
            }
        }

        private void LeftButtonOnClick(object sender, EventArgs e)
        {
            this.ClickedButton = sender as GameButton;

            this.ClickedButton.Visible = false;
        }

        private bool IsEmpty(int buttonX, int buttonY)
        {
            return this.database.GameField[buttonX, buttonY] == 0;
        }

        private bool InBounds(int width, int height, int row, int k, int col, int l)
        {
            return row + k >= 0 && col + l >= 0 && row + k < width && col + l < height;
        }

        private bool IsMine(int row, int k, int col, int l)
        {
            return this.Database.GameField[row + k, col + l] == this.MineValue;
        }

        private void RemoveEmptyLabels(int buttonX, int buttonY)
        {
            throw new NotImplementedException();
        }
    }
}
