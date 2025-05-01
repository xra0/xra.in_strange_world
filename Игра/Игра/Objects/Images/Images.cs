using System.Drawing;

namespace Игра.Objects.Images
{
    internal static class Images
    {
        private static Image playerImage;
        private static Image wallImage;
        private static Image collectibleImage;
        private static Image moneyImage;
        private static Image enemyImage;
        private static Image cactusImage;
        private static Image shopImage;

        public static void LoadImages()
        {
            PlayerImage = Image.FromFile(GamePath.PlayerImage);
            WallImage = Image.FromFile(GamePath.WallImage);
            CollectibleImage = Image.FromFile(GamePath.CollectibleImage);
            MoneyImage = Image.FromFile(GamePath.MoneyImage);
            EnemyImage = Image.FromFile(GamePath.EnemyImage);
            CactusImage = Image.FromFile(GamePath.CactusImage);
            ShopImage = Image.FromFile(GamePath.ShopImage);
        }

        public static Image PlayerImage
        {
            get { return playerImage; }
            set { playerImage = value; }
        }

        public static Image WallImage
        {
            get { return wallImage; }
            set { wallImage = value; }
        }

        public static Image CollectibleImage
        {
            get { return collectibleImage; }
            set { collectibleImage = value; }
        }

        public static Image MoneyImage
        {
            get { return moneyImage; }
            set { moneyImage = value; }
        }

        public static Image EnemyImage
        {
            get { return enemyImage; }
            set { enemyImage = value; }
        }

        public static Image CactusImage
        {
            get { return cactusImage; }
            set { cactusImage = value; }
        }

        public static Image ShopImage
        {
            get { return shopImage; }
            set { shopImage = value; }
        }
    }
}
