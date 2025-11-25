using SplashKitSDK;

namespace FreewayClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Window window = new Window("Freeways Clone - SplashKit", 800, 600);
            GameManager game = new GameManager();
            
            Color darkModeBg = Color.RGBAColor(30, 30, 35, 255); 

            while (!window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                
                game.HandleInput();
                game.Update();
                
                window.Clear(darkModeBg);
                game.Draw();
                window.Refresh(60);
            }
        }
    }
}