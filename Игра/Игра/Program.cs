using System;

namespace Игра
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(50, 50);

            Player player = new Player(2, 2, 10, 10);

            Console.WriteLine("Привет! Давай знакомиться! Как тебя зовут? А хотя не важно, сыграй в игру!");
            Console.CursorVisible = false;
            
            Console.ReadKey();

            char[,] map = Game.ReadMap();

            Console.Clear();
            while (true)
            {
                player.moneyText = Correct.CorrectSpell(player.Money.ToString(), new string[] { "Рубль", "Рубля", "Рублей" });
                player.donutsText = Correct.CorrectSpell(player.Yummy.ToString(), new string[] { "вкусняшка", "вкусняшки", "вкусняшек" });
                
                Game.DrawMap(map);
                
                player.DrawPlayer('@');
                player.DrawInventory();
                player.DrawHealthBar(0, 36);
                
                player.ControlPlayer(map);

                if (map[player.X, player.Y] != ' ')
                {
                    player.Extra(player, map);
                }
                
                Console.Clear();
                if (player.Health == 0)
                {
                    Game.ReadBestScore(player, ConsoleColor.Red, "Ты умер! Игра окончена! ");
                    break;
                }

                if (player.Yummy == 18)
                {
                    Game.ReadBestScore(player, ConsoleColor.Green, "Ты собрал всё! Молодец! ");
                    break;
                }
            }
        }
    }
}


