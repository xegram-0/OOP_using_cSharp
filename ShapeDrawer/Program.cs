using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World!");
            //Shape myShape = new Shape(176);
            Shape theCircle = new Shape(50); //Verification: Circle with 50 pixel
            Drawing myDrawing = new Drawing();
            Point2D mousePos = SplashKit.MousePosition();
            Window window = new Window("Shape Drawer", 600, 600);
            
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                
                //myShape.Draw();
                theCircle.UpdateCircle();
                
                // Move the shape based on the position of the mouse
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    /*
                    if (theCircle.IsMouseInsideCircle())
                    {
                        Console.WriteLine("The circle is in");
                    }
                    else
                    {
                        Console.WriteLine("The circle is out");
                    }
                    */
                    Shape newShape = new Shape(10);
                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();
                    myDrawing.AddShape(newShape);

                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(mousePos);
                }
                theCircle.DrawCircle();
                // If mouse is inside and spacebar is pressed, the color is changed
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    /*
                    
                    
                    bool isMouseInside = mousePos.X >= myShape.X && mousePos.X <= myShape.X + myShape.Width &&
                                         mousePos.Y >= myShape.Y && mousePos.Y <= myShape.Y + myShape.Height;
                    if (isMouseInside)
                    {
                        myShape.Color = SplashKit.RandomColor();
                    }
                    */
                    myDrawing.Background = Color.Random();
                }
                myDrawing.Draw();
                //myShape.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}