namespace MinesweeperLike.App.Forms
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Core;
    using MinesweeperLike.App.Data;

    public partial class GameForm : Form
    {
        private const int TemporarilyGameFieldSize = 9;

        private Button clickedButton;

        private readonly Database database;

        private readonly Engine engine;

        private readonly GameController gameController;

        private Label labelToShow;

        private int time;

        public GameForm()
        {
            this.InitializeComponent();
            this.database = new Database(
                new Button[TemporarilyGameFieldSize, TemporarilyGameFieldSize], 
                new int[TemporarilyGameFieldSize, TemporarilyGameFieldSize], 
                new Label[TemporarilyGameFieldSize, TemporarilyGameFieldSize]);

            this.gameController = new GameController(this.database, this);
            this.engine = new Engine(this.database, this.gameController);

            this.engine.Run();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Restart(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Timer(object sender, EventArgs e)
        {
            this.time++;
            this.labelTimeCounter.Text = this.time.ToString();
        }
    }
}