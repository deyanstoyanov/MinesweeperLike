namespace MinesweeperLike.App.Core
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class GameController : IGameController
    {
        private Button clickedButton;

        private IDatabase database;

        private Label labelToShow;

        private Timer timer;

        private int time;

        private Form gameField;

        public GameController(IDatabase database, Form gameField)
        {
            this.ClickedButton = new Button();
            this.Database = database;
            this.LabelToShow = new Label();
            this.GameField = gameField;
            this.Time = time;
            this.timer = new Timer();
        }

        public Button ClickedButton
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

        public void ButtonOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LoadButtonsToGameField()
        {
            for (int i = 0; i < this.Database.Buttons.GetLength(0); i++)
            {
                for (int j = 0; j < this.Database.Buttons.GetLength(1); j++)
                {
                    Button currentButton = this.Database.Buttons[i, j];
                    currentButton.Click += this.ButtonOnClick;
                    this.GameField.Controls.Add(currentButton);
                }
            }
        }

        public void LoadLabelToGameField(Label label)
        {
            throw new NotImplementedException();
        }

        public void IncreaseTimer(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
