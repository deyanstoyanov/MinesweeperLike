namespace MinesweeperLike.App.Contracts
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public interface IGameButton : IButtonControl, 
                                   IDropTarget, 
                                   ISynchronizeInvoke, 
                                   IWin32Window, 
                                   IBindableComponent, 
                                   IComponent, 
                                   IDisposable
    {
        int Row { get; }

        int Col { get; }

        int LocationX { get; }

        int LocationY { get; }

        bool Visible { get; set; }
    }
}