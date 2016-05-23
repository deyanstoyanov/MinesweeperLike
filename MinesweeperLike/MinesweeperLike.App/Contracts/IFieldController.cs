namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IFieldController
    {
        IDatabase Database { get; }

        IFieldGenerator FieldGenerator { get; }

        void ClickedOnMine(int buttonCoordinateX, int buttonCoordinateY);

        void ClickedOnEmpty(int buttonCoordinateX, int buttonCoordinateY);

        void CreateGameField(
            Form form, 
            Panel panel, 
            MouseEventHandler mouseEventHandler, 
            int minesCount, 
            int gameFieldWidth, 
            int gameFieldHeight);
    }
}