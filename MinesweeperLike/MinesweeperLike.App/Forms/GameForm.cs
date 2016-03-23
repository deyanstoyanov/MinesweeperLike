namespace MinesweeperLike.App.Forms
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core;
    using MinesweeperLike.App.Data;

    public partial class GameForm : Form
    {
        private readonly IDatabase database;

        private readonly IEngine engine;

        private readonly IGameController gameController;

        private int time;

        public GameForm()
        {
            this.InitializeComponent();
            this.database = new Database();

            this.gameController = new GameController(this.database, this);
            this.engine = new Engine(this.database, this.gameController, this);

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