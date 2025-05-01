using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Игра.Objects.Images;

namespace Игра
{
    public partial class GameForm : Form
    {
        private Player player;
        private char[,] map;

        private Panel shopPanel;
        private ListBox lstFoods;
        private Button btnBuy;
        private Button btnClose;
        private Label lblMoney;
        private Food[] foods = new FoodList().GetListFood;
        private bool isShopOpen = false; // Флаг для отслеживания состояния магазина
        private int totalCollectibles = 0; // Общее количество вкусняшек
        private List<Enemy> enemies = new List<Enemy>(); // Список врагов

        public GameForm()
        {
            DoubleBuffered = true;
            InitializeComponent();
            this.KeyDown += GameForm_KeyDown;

            player = new Player(5, 5, 10, 10);
            LoadMap(GamePath.MapFile);
            // Загрузка изображений
            Images.LoadImages();

            InitializeShop();
        }

        private void InitializeShop()
        {
            // Панель магазина
            shopPanel = new Panel
            {
                Size = new Size(400, 300),
                Location = new Point(200, 100),
                BackColor = Color.LightGray,
                Visible = false
            };
            this.Controls.Add(shopPanel);

            // Метка для отображения денег
            lblMoney = new Label
            {
                Text = $"Деньги: {player.GetMoney}",
                Location = new Point(10, 10),
                AutoSize = true
            };
            shopPanel.Controls.Add(lblMoney);

            // Список товаров
            lstFoods = new ListBox
            {
                Location = new Point(10, 40),
                Size = new Size(360, 150)
            };
            shopPanel.Controls.Add(lstFoods);

            // Кнопка "Купить"
            btnBuy = new Button
            {
                Text = "Купить",
                Location = new Point(10, 200),
                Size = new Size(100, 30)
            };
            btnBuy.Click += BtnBuy_Click;
            shopPanel.Controls.Add(btnBuy);

            // Кнопка "Закрыть"
            btnClose = new Button
            {
                Text = "Закрыть",
                Location = new Point(120, 200),
                Size = new Size(100, 30)
            };
            btnClose.Click += (s, e) =>
            {
                shopPanel.Visible = false;
                isShopOpen = false; // Сбрасываем флаг
                this.Focus(); // Возвращаем фокус на основную форму
            };
            shopPanel.Controls.Add(btnClose);

            foreach (var food in foods)
            {
                lstFoods.Items.Add($"{food.GetName} - {food.GetPrice} руб.");
            }
        }

        private void BtnBuy_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstFoods.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < foods.Length)
            {
                var food = foods[selectedIndex];
                if (player.GetMoney >= food.GetPrice && food.GetPrice > 0)
                {
                    player.GetMoney -= food.GetPrice;
                    player.GetHealth += food.GetHealth;
                    food.GetPrice = -1; // Товар куплен
                    lblMoney.Text = $"Деньги: {player.GetMoney}";
                    lstFoods.Items[selectedIndex] = $"{food.GetName} - Нет в наличии";
                    MessageBox.Show($"Вы купили {food.GetName}!");
                }
                else
                {
                    MessageBox.Show("Недостаточно денег или товар недоступен.");
                }
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Point oldPosition = new Point(player.GetY, player.GetX); // Сохраняем старую позицию

            player.ControlPlayer(map, e);

            Enemy.ControlEnemy(map, enemies); // Двигаем врагов

            Point newPosition = new Point(player.GetY, player.GetX); // Новая позиция

            // Обновляем только старую и новую позиции
            UpdatePlayerPosition(oldPosition, newPosition);

            // Проверяем взаимодействия
            CheckInteraction();
            Invalidate();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            int cellSize = 20; // Размер одной клетки карты

            // Отрисовка карты
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Image image = null;

                    switch (map[i, j])
                    {
                        case '■': image = Images.WallImage; break; // Стена
                        case '*': image = Images.CollectibleImage; break; // Вкусняшка
                        case '$': image = Images.MoneyImage; break; // Деньги
                        case 'K': image = Images.CactusImage; break; // Кактус
                        case '&': image = Images.ShopImage; break; // Магазин
                    }

                    if (image != null)
                    {
                        g.DrawImage(image, j * cellSize, i * cellSize, cellSize, cellSize);
                    }
                }
            }

            // Отрисовка врагов
            foreach (var enemy in enemies)
            {
                g.DrawImage(Images.EnemyImage, enemy.GetY * cellSize, enemy.GetX * cellSize, cellSize, cellSize);
            }

            // Отрисовка игрока
            g.DrawImage(Images.PlayerImage, player.GetY * cellSize, player.GetX * cellSize, cellSize, cellSize);

            DrawGraphics(e); // Отрисовка графики (здоровье, инвентарь и т.д.)
        }

        private void DrawGraphics(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // Отображение здоровья и инвентаря
            int barWidth = 100;
            int barHeight = 15;

            // Здоровье
            g.FillRectangle(Brushes.Red, new Rectangle(map.Length, 10, barWidth, barHeight));
            g.FillRectangle(Brushes.Green, new Rectangle(map.Length, 10, (int)(barWidth * ((double)player.GetHealth / player.GetMaxHealth)), barHeight));

            // Вкусняшки и деньги
            g.DrawImage(Images.CollectibleImage, map.Length - 30, 30, 15, 15);
            g.DrawString(player.GetYummy.ToString(), new Font("Consolas", 12), Brushes.White, new PointF(map.Length, 30));

            g.DrawImage(Images.MoneyImage, map.Length - 30, 50, 15, 15);
            g.DrawString(player.GetMoney.ToString(), new Font("Consolas", 12), Brushes.White, new PointF(map.Length, 50));

            // Отображение лучшего рекорда внизу справа
            string bestScoreText = $"Лучший рекорд: {player.BestScore}";
            SizeF textSize = g.MeasureString(bestScoreText, new Font("Consolas", 12));
            PointF position = new PointF(this.ClientSize.Width - textSize.Width - 10, this.ClientSize.Height - textSize.Height - 10);
            g.DrawString(bestScoreText, new Font("Consolas", 12), Brushes.White, position);
        }

        private void CheckInteraction()
        {
            if (map[player.GetX, player.GetY] != ' ')
            {
                player.Interaction(map, isShopOpen, shopPanel);

                // Проверка здоровья игрока
                if (player.GetHealth <= 0)
                {
                    EndGame();
                    return; // Прекращаем дальнейшую обработку
                }

                // Проверка победы
                if (player.GetYummy == totalCollectibles)
                {
                    WinGame();
                    return; // Прекращаем дальнейшую обработку
                }

                foreach (var enemy in enemies)
                {
                    if (enemy.GetX == player.GetX && enemy.GetY == player.GetY)
                    {
                        player.GetHealth--; // Уменьшаем здоровье игрока
                        if (player.GetHealth <= 0)
                        {
                            EndGame();
                            return;
                        }
                    }
                }

                Refresh(); // Перерисовка карты
            }
        }

        private void LoadMap(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Length;

            map = new char[rows, cols];
            totalCollectibles = 0; // Сбрасываем счётчик вкусняшек
            enemies.Clear(); // Очищаем список врагов

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = lines[i][j];
                    if (map[i, j] == '*') // Если это вкусняшка
                    {
                        totalCollectibles++;
                    }
                    else if (map[i, j] == 'E') // Если это враг
                    {
                        enemies.Add(new Enemy(i, j, 10, 10, 1, GamePath.EnemyImage)); // Добавляем врага
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EndGame()
        {
            // Отключаем обработку клавиш
            this.KeyDown -= GameForm_KeyDown;

            // Получаем текущий счёт игрока
            // Получаем текущий счёт игрока
            int currentScore = player.GetMoney + player.GetYummy;


            player.BestScore = player.BestScore > currentScore ? currentScore : player.BestScore;

            // Отображаем сообщение пользователю
            var result = MessageBox.Show($"Игра окончена! Ваше здоровье достигло 0.\n" +
                                         $"Ваш счёт: {currentScore}. Лучший счёт: {player.BestScore}.\n" +
                                         "Хотите начать заново?",
                                         "Конец игры",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Application.Exit(); // Закрываем приложение
            }
        }
        private void RestartGame()
        {
            // Сбрасываем состояние игрока
            player = new Player(5, 5, 10, 10);

            // Перезагружаем карту
            LoadMap("map.txt");

            // Включаем обработку клавиш
            this.KeyDown += GameForm_KeyDown;

            // Обновляем интерфейс
            Refresh();
        }

        private void WinGame()
        {
            // Отключаем обработку клавиш
            this.KeyDown -= GameForm_KeyDown;

            // Получаем текущий счёт игрока
            int currentScore = player.GetMoney;

            // Получаем лучший счёт из файла
            int bestScore = player.BestScore;

            this.DoubleBuffered = true;
            var result = MessageBox.Show($"Поздравляем! Вы собрали все вкусняшки!\n" +
                                         $"Ваш счёт: {currentScore}. Лучший счёт: {bestScore}.\n" +
                                         "Хотите начать заново?",
                                         "Победа!",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Application.Exit(); // Закрываем приложение
            }
        }
        private void UpdatePlayerPosition(Point oldPosition, Point newPosition)
        {
            using (Graphics g = this.CreateGraphics())
            {
                int cellSize = 20; // Размер клетки карты

                // Восстановление содержимого на старой позиции
                char oldCell = map[oldPosition.Y, oldPosition.X];
                Image oldImage = null;

                switch (oldCell)
                {
                    case '■': oldImage = Images.WallImage; break; // Стена
                    case '*': oldImage = Images.CollectibleImage; break; // Вкусняшка
                    case '$': oldImage = Images.MoneyImage; break; // Деньги
                    case 'E': oldImage = Images.EnemyImage; break; // Враг
                    case 'K': oldImage = Images.CactusImage; break; // Кактус
                    case '&': oldImage = Images.ShopImage; break; // Магазин
                }

                if (oldImage != null)
                {
                    // Рисуем изображение, соответствующее содержимому карты
                    g.DrawImage(oldImage, oldPosition.X * cellSize, oldPosition.Y * cellSize, cellSize, cellSize);
                }
                else
                {
                    // Если клетка пустая, закрашиваем её цветом фона
                    g.FillRectangle(Brushes.Silver, oldPosition.X * cellSize, oldPosition.Y * cellSize, cellSize, cellSize);
                }

                // Рисование персонажа на новой позиции
                Image playerImage = Images.PlayerImage;
                g.DrawImage(playerImage, newPosition.X * cellSize, newPosition.Y * cellSize, cellSize, cellSize);
            }
        }
        private void MoveEnemies()
        {
            
        }
    }
}
