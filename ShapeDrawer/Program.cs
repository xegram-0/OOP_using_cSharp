using SplashKitSDK;

namespace ShapeDrawer;

    
    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }
        public static void Main()
        {
            ShapeKind kindToAdd = ShapeKind.Circle;
            Drawing myDrawing = new Drawing();
            
            Window window = new Window("Shape Drawer", 600, 600);
            
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }
                // Move the shape based on the position of the mouse
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape;
                    switch (kindToAdd)
                    {
                        case ShapeKind.Circle:
                            newShape = new MyCircle();
                            break;
                        case ShapeKind.Line:
                            newShape = new MyLine();
                            break;
                        default:
                            newShape = new MyRectangle();
                            break;
                    }
                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();
                    myDrawing.AddShape(newShape);
                }
                
                myDrawing.Draw();
                
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(SplashKit.MousePosition());
                }
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = Color.Random();
                }
                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    List<Shape> selectedShape = myDrawing.SelectedShapes;
                    if (selectedShape.Count > 0)
                    {
                        // Remove the last selected shape
                        Shape lastSelected = selectedShape[selectedShape.Count - 1];
                        myDrawing.RemoveShape(lastSelected);
                    }
                }

                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    myDrawing.Save("TestDrawing2.txt");
                }
                SplashKit.RefreshScreen();
                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    try{myDrawing.Load("TestDrawing.txt");} //if the file existed with the right name
                    catch(Exception e) {Console.Error.WriteLine("Loading file error {0}", e.Message);}
                }
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }

