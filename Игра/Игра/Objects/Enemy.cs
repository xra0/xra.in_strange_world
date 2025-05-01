using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Игра
{
    class Enemy : Person
    {
        private int Damage { get; set; }
        //private int Money { get; set; } // Оставим на будещее

        public Enemy(int x, int y, int health, int maxHealth, int damage, string image)
        {
            GetX = x;
            GetY = y;
            GetHealth = health;
            GetMaxHealth = maxHealth;
            Damage = damage;
            GetImage = Image.FromFile(GamePath.EnemyImage);
        }

        public int GetDamage
        {
            get { return Damage; }
            set { Damage = value; }
        }

        public static void ControlEnemy(char[,] map, List<Enemy> enemies)
        {
            Random random = new Random();

            foreach (var enemy in enemies)
            {   
                // Случайное направление: 0 - вверх, 1 - вниз, 2 - влево, 3 - вправо

                int newX = enemy.GetX;
                int newY = enemy.GetY;

                switch (random.Next(4))
                {
                    case 0: newX--; break; // Вверх
                    case 1: newX++; break; // Вниз
                    case 2: newY--; break; // Влево
                    case 3: newY++; break; // Вправо
                }

                // Проверяем, можно ли переместиться
                if (newX >= 0 && newX < map.GetLength(0) && newY >= 0 && newY < map.GetLength(1) && map[newX, newY] == ' ')
                {
                    map[enemy.GetX, enemy.GetY] = ' '; // Очищаем старую позицию врага
                    map[newX, newY] = 'E'; // Перемещаем врага
                    enemy.GetX = newX;
                    enemy.GetY = newY;
                }
            }
        }
    }
}
