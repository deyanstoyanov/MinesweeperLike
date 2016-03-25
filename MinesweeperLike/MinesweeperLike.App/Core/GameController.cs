namespace MinesweeperLike.App.Core
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core.Factories;
    using MinesweeperLike.App.Models;

    public class GameController : IGameController
    {
        private GameButton clickedButton;

        private IDatabase database;

        private IButtonFactory buttonFactory;

        private ILabelFactory labelFactory;

        private Label labelToShow;

        private Timer timer;

        private int time;

        private Form gameField;

        public GameController(IDatabase database, Form gameField)
        {
            this.Database = database;
            this.GameField = gameField;
            this.buttonFactory = new ButtonFactory();
            this.labelFactory = new LabelFactory();
            this.ClickedButton = new GameButton();
            this.LabelToShow = new Label();
            this.Time = time;
            this.timer = new Timer();
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

        private void LeftButtonOnClick(object sender, EventArgs mouseEventArgs)
        {
            this.ClickedButton = sender as GameButton;

            this.ClickedButton.Hide();
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
    }
}
