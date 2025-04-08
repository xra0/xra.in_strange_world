using System;
using System.IO;
using System.Threading;


namespace Игра
{
    static class Game
    {
        public static string[] MapLines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\map.txt");
        public static string[] best = File.ReadAllText(Directory.GetCurrentDirectory() + "\\bestscore.txt").Split(' ');


        public static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static char[,] ReadMap()
        {
            char[,] map = new char[MapLines.Length, MapLines[0].Length];
            for (int i = 0; i < MapLines.Length; i++)
            {
                for (int j = 0; j < MapLines[i].Length; j++)
                {
                    map[i, j] = MapLines[i][j];
                }
            }
            return map;
        }

        public static void ReadBestScore(Player player, ConsoleColor consoleColor, string text)
        {
            Console.BackgroundColor = consoleColor;
            Console.WriteLine($"{text}Твой счет: {player.Money + player.Yummy}. Предыдущий счет: {best[0]}. Лучший счет: " +
                $"{((player.Money + player.Yummy > Convert.ToInt32(best[1])) ? player.Money + player.Yummy : Convert.ToInt32(best[1]))}");
            Game.WriteBestScore(player);
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        public static void WriteBestScore(Player player)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\bestscore.txt", $"{player.Money + player.Yummy} " +
                $"{((player.Money + player.Yummy > Convert.ToInt32(best[1])) ? player.Money + player.Yummy : Convert.ToInt32(best[1]))}");
        }
    }
}
