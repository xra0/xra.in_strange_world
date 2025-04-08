using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра
{
    class Food
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public Food(int price, string name, int health)
        {
            Price = price;
            Name = name;
            Health = health;
        }
    }
}
