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

        public static bool Buy(Player player, Food[] foods, int choice)
        {
            if (choice == 5) return false;
            if (player.Money >= foods[choice].Price && foods[choice].Price != -1)
            {
                Console.WriteLine($"Ты купил {foods[choice].Name} за {foods[choice].Price} {Correct.CorrectSpell(foods[choice].Price.ToString(), new string[] { "Рубль", "Рубля", "Рублей" })}.");
                player.Money -= foods[choice].Price;
                player.Health += foods[choice].Health;
                foods[choice].Price = -1;
                return true;
            }
            else
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("Товара нет в наличии или вы нищеброд.");
                return true;
            }
        }

        public static void Shop(Player player, string moneyText)
        {
            Console.Clear();

            Food[] foods = new Food[]
            {
                new Food(100, "Шишка сосновая обыкновенная", 1),
                new Food(200, "Водка \"Столичная\" 200 мл", -1),
                new Food(300, "Трактор 3000 Pro Max Ultra G7 OC A28P3000RU", -3),
                new Food(400, "MILK 0 мл", 5),
                new Food(500, "Дед Славянский Оригинальный 1950 года Пробег 200000 км", 10),
                new Food(0, "Выход", 1)
            };

            Cursor cursor = new Cursor(75, 5);
            bool isShop = true;

            while (isShop)
            {
                Console.Clear();
                Console.WriteLine("Магазин. Чтобы выбрать нажмите Enter.\n" + new string('_', 73) + "\nТовары:\n");
                foreach (var food in foods)
                {
                    Console.WriteLine(new string('_', 73) + $"\n{food.Name,-60} - {food.Price} {moneyText}");
                }

                cursor.DrawCursor("<-");
                player.DrawInventory();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursor.Choice > 0)
                        {
                            cursor.Choice--;
                            cursor.Y -= 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursor.Choice < foods.Length - 1)
                        {
                            cursor.Choice++;
                            cursor.Y += 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        isShop = Buy(player, foods, cursor.Choice);
                        break;
                }
            }
        }
    }
}
