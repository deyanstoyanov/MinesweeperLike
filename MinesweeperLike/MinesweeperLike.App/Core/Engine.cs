namespace MinesweeperLike.App.Core
{
    using MinesweeperLike.App.Contracts;

    public class Engine : IEngine
    {
        private IDatabase database;

        private IGameController gameController;

        public Engine(IDatabase database, IGameController gameController)
        {
            this.database = database;
            this.gameController = gameController;
        }

        public void Run()
        {
            this.database.AddButtons();
            this.gameController.LoadButtonsToGameField();
        }
    }
}
