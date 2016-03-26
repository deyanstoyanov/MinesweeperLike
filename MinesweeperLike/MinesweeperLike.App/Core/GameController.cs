namespace MinesweeperLike.App.Core
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;
    using MinesweeperLike.App.Models;

    using sweeperLike.App.Constants;

    public class GameController : IGameController
    {
        private GameButton clickedButton;

        private IDatabase database;

        private readonly IButtonFactory buttonFactory;

        private readonly ILabelFactory labelFactory;

        private readonly IMineFactory mineFactory;

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
            throw new NotImplementedException();
        }

        private void LeftButtonOnClick(object sender, EventArgs e)
        {
            this.ClickedButton = sender as GameButton;

            this.ClickedButton.Hide();
        }

        private void RemoveEmptyLabels(int buttonX, int buttonY)
        {
            throw new NotImplementedException();
        }
    }
}
