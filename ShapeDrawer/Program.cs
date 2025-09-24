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
                
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}