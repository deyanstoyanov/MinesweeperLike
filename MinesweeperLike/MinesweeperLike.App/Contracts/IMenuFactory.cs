namespace MinesweeperLike.App.Contracts
{
    using System.Windows.Forms;

    public interface IMenuFactory
    {
        MenuStrip CrateMenu(string name);
    }
}