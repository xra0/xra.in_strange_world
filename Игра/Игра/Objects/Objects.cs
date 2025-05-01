using System.Drawing;

namespace Игра
{
    class Object
    {
        private int X { get; set; }
        private int Y { get; set; }
        private Image Image { get; set; }

        public int GetX
        {
            get { return X; }
            set { X = value; }
        }

        public int GetY
        {
            get { return Y; }
            set { Y = value; }
        }

        public Image GetImage
        {
            get { return Image; }
            set { Image = value; }
        }
    }

    class Person : Object
    {
        private int Health { get; set; }
        private int MaxHealth { get; set; }

        public int GetHealth
        {
            get { return Health; }
            set { Health = value; }
        }

        public int GetMaxHealth
        {
            get { return MaxHealth; }
            set { MaxHealth = value; }
        }
    }
}
