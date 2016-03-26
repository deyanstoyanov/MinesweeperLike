namespace MinesweeperLike.App.Core
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;
    using MinesweeperLike.App.Data;

    public class Engine : IEngine
    {
        private IDatabase database;

        private IGameController gameController;

        private Form form;

        public Engine(Form form)
        {
            this.form = form;
            this.database = new Database();
            this.gameController = new GameController(this.database, form);
        }

        public void Run()
        {
            this.Execute();
        }

        private void Execute()
        {
            this.gameController.CreateButtons(this.form);
            this.gameController.CreateLabels(this.form);
            this.gameController.CreateMines();
        }
    }
}
