using System;

namespace Игра
{
    class Object
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Cursor : Object
    {
        public int Choice = 0;

        public Cursor(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void DrawCursor(string image)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(image);
        }
    }

    class Person : Object
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
    }

    class Player : Person
    {
        public int Yummy, Money;
        public string donutsText;
        public string moneyText;

        public Player(int x, int y, int health, int maxHealth)
        {
            X = x;
            Y = y;
            Health = health;
            MaxHealth = maxHealth;
        }

        public void DrawHealthBar(int x, int y)
        {
            string healthbar = "";

            ConsoleColor defaultColor = Console.BackgroundColor;

            for (int i = 0; i < Health; i++)
                healthbar += " ";

            Console.SetCursorPosition(x, y);
            Console.Write('|');
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(healthbar);
            Console.BackgroundColor = defaultColor;

            healthbar = "";
            for (int i = Health; i < MaxHealth; i++)
                healthbar += " ";
            Console.Write(healthbar + "|");
        }

        public void DrawPlayer(char image)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(image);
        }

        public void DrawInventory()
        {
            Console.SetCursorPosition(0, 30);
            Console.WriteLine($"\nЗадача: Перед тобой поле. Нужно собрать все вкусняшки(*)!\nИнвентарь: \n{Yummy} {donutsText}," +
                    $"\nДеньги: {Money} {moneyText}\nЗдоровье:");
        }

        public void ControlPlayer(char[,] map)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (map[X - 1, Y] != '■') X--; break;
                case ConsoleKey.DownArrow:
                    if (map[X + 1, Y] != '■') X++; break;
                case ConsoleKey.LeftArrow:
                    if (map[X, Y - 1] != '■') Y--; break;
                case ConsoleKey.RightArrow:
                    if (map[X, Y + 1] != '■') Y++; break;
            }
        }

        public void Extra(Player player, char[,] map)
        {
            switch (map[X, Y])
            {
                case '*': Yummy++; break;
                case 'K': Health--; break;
                case '$': Money += 100; break;
                case '&': Game.Shop(player, moneyText); break;
            }
            map[X, Y] = ' ';
        }
    }
}
