using System;

namespace Игра
{
    class Object
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Image { get; set; }
    }

    class Cursor : Object
    {
        public int Choice = 0;

        public Cursor(int x, int y, string image)
        {
            X = x;
            Y = y;
            Image = image;
        }
        public void DrawCursor()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Image);
        }
    }

    class Person : Object
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
    }
}
