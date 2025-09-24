using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World!");
            Shape myShape = new Shape(176);
            Window window = new Window("Shape Drawer", 600, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                
                myShape.Draw();
                // Move the shape based on the position of the mouse
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }
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
                myShape.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}