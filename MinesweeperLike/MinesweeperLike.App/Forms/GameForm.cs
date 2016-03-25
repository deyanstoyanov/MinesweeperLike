namespace MinesweeperLike.App.Forms
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Core;

    public partial class GameForm : Form
    {
        private readonly IEngine engine;

        private int time;

        public GameForm()
        {
            this.InitializeComponent();
            this.engine = new Engine(this);
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