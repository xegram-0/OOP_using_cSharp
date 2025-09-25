using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World!");
            Shape myShape = new Shape(176);
            
            Shape theCircle = new Shape(50); //Verification: Circle with 50 pixel
            
            Window window = new Window("Shape Drawer", 600, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                
                myShape.Draw();
                theCircle.UpdateCircle();
                
                // Move the shape based on the position of the mouse
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                    /*
                    Point2D mousePos = SplashKit.MousePosition();

                    double distance = Math.Sqrt(Math.Pow((mousePos.X - theCircle.X),2) +  Math.Pow((mousePos.Y - theCircle.Y),2));
                    if (distance <= 50)
                    {
                        Console.WriteLine("The circle is in");
                    }
                    else
                    {
                        Console.WriteLine("The circle is out");
                    }


                    double dx = mousePos.X - (theCircle.X+50);
                    double dy = mousePos.Y - (theCircle.Y+50);
                    double distSquared = dx * dx + dy * dy;
                    if (distSquared <= (50 * 50))
                    {
                        Console.WriteLine("The circle is in");
                    }
                    else
                    {
                        Console.WriteLine("The circle is out");
                    }
                    */
                    if (theCircle.IsMouseInsideCircle())
                    {
                        Console.WriteLine("The circle is in");
                    }
                    else
                    {
                        Console.WriteLine("The circle is out");
                    }
                }
                theCircle.DrawCircle();
                // If mouse is inside and spacebar is pressed, the color is changed
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    Point2D mousePos = SplashKit.MousePosition();
                    bool isMouseInside = mousePos.X >= myShape.X && mousePos.X <= myShape.X + myShape.Width &&
                                         mousePos.Y >= myShape.Y && mousePos.Y <= myShape.Y + myShape.Height;
                    if (isMouseInside)
                    {
                        myShape.Color = SplashKit.RandomColor();
                    }
                }
                //myShape.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}