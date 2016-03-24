namespace MinesweeperLike.App.Core
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class Engine : IEngine
    {
        private IDatabase database;

        private IGameController gameController;

        private Form form;

        public Engine(IDatabase database, IGameController gameController, Form form)
        {
            this.database = database;
            this.gameController = gameController;
            this.form = form;
        }

        public void Run()
        {
            this.database.AddButtons();
            this.gameController.LoadButtonsToGameField();
            this.database.AddLabels(this.form);
            this.database.AddMines();
        }
    }
}
