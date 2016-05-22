namespace MinesweeperLike.App.Contracts
{
    public interface IMineFactory
    {
        void CreateMine(
            IDatabase database, 
            int mineCoordinateX, 
            int mineCoordinateY, 
            int mineLocationX, 
            int mineLocationY);
    }
}