namespace MinesweeperLike.App.Models
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class GameButton : Button, IGameButton
    {
        public int Row { get; set; }

        public int Col { get; set; }       
    }
}
