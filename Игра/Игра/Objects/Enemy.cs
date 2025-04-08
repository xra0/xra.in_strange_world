using System;

namespace Игра
{
    class Enemy : Person
    {
        private int Damage { get; set; }
        //private int Money { get; set; } // Оставим на будещее
        Random random = new Random();

        public Enemy(int x, int y, int health, int maxHealth, int damage, string image)
        {
            X = x;
            Y = y;
            Health = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Image = image;
        }

        public void DrawEnemy()
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(Image);
        }

        public void ControlEnemy(char[,] map)
        {
            int newX = X;
            int newY = Y;
            // 0 - вверх, 1 - вниз, 2 - влево, 3 - вправо
            switch (random.Next(0, 4))
            {
                case 0: newX--; break;
                case 1: newX++; break;
                case 2: newY--; break;
                case 3: newY++; break;
            }

            if (map[newX, newY] == ' ')
            {
                map[X, Y] = ' ';
                X = newX;
                Y = newY;
                map[X, Y] = Image.ToCharArray()[0];
            }
        }
    }
}
