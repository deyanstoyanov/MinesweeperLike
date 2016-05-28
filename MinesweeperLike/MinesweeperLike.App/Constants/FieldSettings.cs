namespace MinesweeperLike.App.Constants
{
    using System.Drawing;

    public static class FieldSettings
    {
        public const int GameFieldLocationWidth = 29;

        public const int GameFieldLocationHeight = 52;

        public const int GameFieldBorderSize = 3;

        public const string FlagImagePath = @"../../Resources/flag.png";

        public const string NotAMinePath = @"../../Resources/not-a-mine.png";

        public const string Font = "Arial";

        public static readonly Color GameFieldBorderColor = Color.FromArgb(117, 117, 117);
    }
}