using System;
using System.Drawing;

namespace ShapeDrawing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Shape myShape = new Shape(2);
            myShape.Draw();
            myShape.IsAt(1, 1);
            Console.WriteLine(myShape.ComputeArea());
        }
    }
}

