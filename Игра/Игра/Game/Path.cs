using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра
{
    internal static class GamePath
    {
        private static string path = Directory.GetCurrentDirectory() + "\\Objects\\";
        private static string mapFile = Path.Combine(path, "map.txt");
        private static string bestScoreFile = Path.Combine(path, "bestscore.txt");
        private static string playerImage = Path.Combine(path, "player.png");
        private static string wallImage = Path.Combine(path, "wall.png");
        private static string collectibleImage = Path.Combine(path, "donut.png");
        private static string moneyImage = Path.Combine(path, "money.png");
        private static string enemyImage = Path.Combine(path, "enemy.png");
        private static string cactusImage = Path.Combine(path, "cactus.png");
        private static string shopImage = Path.Combine(path, "shop.png");

        public static string MapFile
        {
            get { return mapFile; }
        }
        public static string BestScoreFile
        {
            get { return bestScoreFile; }
        }
        public static string PlayerImage
        {
            get { return playerImage; }
        }
        public static string WallImage
        {
            get { return wallImage; }
        }
        public static string CollectibleImage
        {
            get { return collectibleImage; }
        }
        public static string MoneyImage
        {
            get { return moneyImage; }
        }
        public static string EnemyImage
        {
            get { return enemyImage; }
        }
        public static string CactusImage
        {
            get { return cactusImage; }
        }
        public static string ShopImage
        {
            get { return shopImage; }
        }
        public static string GetPath
        {
            get { return path; }
            set { path = value; }
        }
    }
}
