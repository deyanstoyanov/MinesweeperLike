namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Models;

    public interface IGameController
    {
        IFieldGenerator FieldGenerator { get; }

        IFormGenerator GameFormGenerator { get; }

        IFieldController FieldController { get; }

        Form Form { get; }

        Timer Timer { get; }

        Panel Panel { get; }

        int Time { get; }

        void LeftButtonOnClick(object sender, MouseEventArgs mouseEventArgs);

        void RightButtonOnClick(object sender, MouseEventArgs mouseEventArgs);
    }
}