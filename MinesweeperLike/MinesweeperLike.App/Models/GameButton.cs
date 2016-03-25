namespace MinesweeperLike.App.Models
{
    using System.Windows.Forms;

    using MinesweeperLike.App.Contracts;

    public class GameButton : Button, IGameButton
    {
        private int row;

        private int col;

        private int locationX;

        private int locationY;

        private bool visible;

        public GameButton()
        {
        }

        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }

            set
            {
                this.col = value;
            }
        }

        public int LocationX
        {
            get
            {
                return this.locationX;
            }

            set
            {
                this.locationX = value;
            }
        }

        public int LocationY
        {
            get
            {
                return this.locationY;
            }

            set
            {
                this.locationY = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }
    }
}
