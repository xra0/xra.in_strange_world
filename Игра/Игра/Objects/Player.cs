using System;

namespace Игра
{
    class Player : Person
    {
        public int Yummy, Money;
        public string donutsText;
        public string moneyText;

        public Player(int x, int y, int health, int maxHealth, string image)
        {
            X = x;
            Y = y;
            Health = health;
            MaxHealth = maxHealth;
            Image = image;
        }

        public void DrawHealthBar()
        {
            string healthbar = "";

            ConsoleColor defaultColor = Console.BackgroundColor;

            for (int i = 0; i < Health; i++)
                healthbar += " ";

            Console.SetCursorPosition(0, 36);
            Console.Write('|');
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(healthbar);
            Console.BackgroundColor = defaultColor;

            healthbar = "";
            for (int i = Health; i < MaxHealth; i++)
                healthbar += " ";
            Console.Write(healthbar + "|");
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(Image);
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
            if (map[player.X, player.Y] != ' ')
            {
                switch (map[X, Y])
                {
                    case '*': Yummy++; break;
                    case 'E':
                    case 'K': Health--; break;
                    case '$': Money += 100; break;
                    case '&': Shop.Show(player, moneyText); break;
                }
                if (map[player.X, player.Y] != '&') map[X, Y] = ' ';
            }
        }
    }
}
