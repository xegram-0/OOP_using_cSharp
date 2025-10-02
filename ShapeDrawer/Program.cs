using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            //Shape theCircle = new Shape(50); //Verification: Circle with 50 pixel
            Drawing myDrawing = new Drawing();
            Window window = new Window("Shape Drawer", 600, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                // Move the shape based on the position of the mouse
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape = new Shape(100);
                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();
                    myDrawing.AddShape(newShape);
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    Point2D mousePos = SplashKit.MousePosition();
                    myDrawing.SelectShapesAt(mousePos);
                }
                
                // If mouse is inside and spacebar is pressed, the color is changed
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = Color.Random();
                }

                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    List<Shape> selectedShape = myDrawing.SelectedShapes;
                    /*
                    foreach (Shape shape in selectedShape)
                    {
                        myDrawing.RemoveShape(shape);
                    }
*/
                    if (selectedShape.Count > 0)
                    {
                        // Remove the last selected shape
                        Shape lastSelected = selectedShape[selectedShape.Count - 1];
                        myDrawing.RemoveShape(lastSelected);
                    }
                    
                }
                myDrawing.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}





/*


                    bool isMouseInside = mousePos.X >= myShape.X && mousePos.X <= myShape.X + myShape.Width &&
                                         mousePos.Y >= myShape.Y && mousePos.Y <= myShape.Y + myShape.Height;
                    if (isMouseInside)
                    {
                        myShape.Color = SplashKit.RandomColor();
                    }
                    */