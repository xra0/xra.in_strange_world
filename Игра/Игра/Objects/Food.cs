namespace Игра
{
    class Food
    {
        private int Price { get; set; }
        private string Name { get; set; }
        private int Health { get; set; }
        public Food(int price, string name, int health)
        {
            GetPrice = price;
            GetName = name;
            GetHealth = health;
        }

        public int GetPrice
        {
            get { return Price; }
            set { Price = value; }
        }

        public string GetName
        {
            get { return Name; }
            set { Name = value; }
        }

        public int GetHealth
        {
            get { return Health; }
            set { Health = value; }
        }
    }

    class FoodList
    {
        private Food[] Foods;

        public FoodList()
        {
            Foods =
            [
                new Food(100, "Шишка сосновая обыкновенная", 1),
                new Food(200, "Водка \"Столичная\" 200 мл", -1),
                new Food(300, "Трактор 3000 Pro Max Ultra G7 OC A28P3000RU", -3),
                new Food(400, "MILK 0 мл", 5),
                new Food(500, "Дед Славянский Оригинальный 1950 года Пробег 200000 км", 10)
            ];
        }

        public Food[] GetListFood
        {
            get
            {
                return Foods;
            }
        }
    }
}
