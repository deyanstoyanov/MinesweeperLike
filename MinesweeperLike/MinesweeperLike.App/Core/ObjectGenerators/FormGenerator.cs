namespace MinesweeperLike.App.Core.ObjectGenerators
{
    using System;
    using System.Windows.Forms;

    using MinesweeperLike.App.Constants;
    using MinesweeperLike.App.Contracts;

    public class FormGenerator : IFormGenerator
    {
        private readonly Form form;

        public FormGenerator(IDatabase database, Form form)
        {
            //this.MenuFactory = new MenuFactory();
            this.form = form;
            this.Database = database;
        }

        public IDatabase Database { get; }

        public void FormSize(Form form, Panel panel, int width, int height)
        {
            form.AutoSize = true;
            form.Width = (height * ButtonSettings.ButtonSizeWidth) + FieldSettings.GameFieldLocationWidth + 50;
            form.Height = (width * ButtonSettings.ButtonSizeHeight) + FieldSettings.GameFieldLocationHeight + 70;
            form.FormBorderStyle = FormBorderStyle.Fixed3D;
            form.MaximizeBox = false;
        }

        public void CreateMenu(Form form, EventHandler eventHandler)
        {
            throw new NotImplementedException();
        }

        public void LoadStatusBar()
        {
            throw new NotImplementedException();
        }
    }
}