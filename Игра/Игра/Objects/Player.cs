using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Игра.Objects.Images;

namespace Игра
{
    class Player : Person
    {
        private int Yummy, Money;
        private static int best = Convert.ToInt32(File.ReadAllText(GamePath.BestScoreFile));

        public Player(int x, int y, int health, int maxHealth)
        {
            GetX = x;
            GetY = y;
            GetHealth = health;
            GetMaxHealth = maxHealth;
            GetImage = Images.PlayerImage;
        }

        public int GetYummy
        {
            get { return Yummy; }
            set { Yummy = value; }
        }

        public int GetMoney
        {
            get { return Money; }
            set { Money = value; }
        }

        public void ControlPlayer(char[,] map, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (map[GetX - 1, GetY] != '■') GetX--;
                    break;
                case Keys.Down:
                    if (map[GetX + 1, GetY] != '■') GetX++;
                    break;
                case Keys.Left:
                    if (map[GetX, GetY - 1] != '■') GetY--;
                    break;
                case Keys.Right:
                    if (map[GetX, GetY + 1] != '■') GetY++;
                    break;
            }
        }

        public void Interaction(char[,] map, bool isShopOpen, Panel shopPanel)
        {
            switch (map[GetX, GetY])
            {
                case '*':
                    Yummy++;
                    break;
                case 'E':
                case 'K':
                    GetHealth--;
                    break;
                case '$':
                    Money += 100;
                    break;
                case '&':
                    if (!isShopOpen) // Проверяем, открыт ли магазин
                    {
                        isShopOpen = true; // Устанавливаем флаг
                        shopPanel.Visible = true;
                        shopPanel.BringToFront(); // Перемещаем панель на передний план
                    }
                    return; // Прекращаем дальнейшую обработку
            }
            if (map[GetX, GetY] != '&' && map[GetX, GetY] != 'E')
            {
                map[GetX, GetY] = ' '; // Убираем объект с карты
            }
        }

        public int BestScore
        {
            get
            {
                return best;
            }
            set
            {
                File.WriteAllText(GamePath.BestScoreFile,
                $"{((GetMoney + GetYummy > best) ? GetMoney + GetYummy : best)}");

            }
        }
    }
}
