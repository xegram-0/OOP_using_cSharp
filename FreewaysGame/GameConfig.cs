using SplashKitSDK;

namespace FreewaysGame
{
    public static class GameConfig
    {
        // Window Settings
        public const int ScreenWidth = 1024;
        public const int ScreenHeight = 768;
        public const string WindowTitle = "Freeways: SplashKit Edition";
        public const int TargetFPS = 60;

        // Colors
        public static Color BackgroundColor = Color.RGBColor(59, 143, 110); // Classic Green
        public static Color RoadColor = Color.RGBColor(80, 80, 80);       // Asphalt Grey
        public static Color PreviewColor = Color.White;                   // Drawing line
        
        // Gameplay
        public const float RoadWidth = 30.0f;
        
        // CHANGED: Reduced from 10.0f to 4.0f to make drawing more responsive
        public const float DrawSmoothing = 4.0f; 
        
        public const float CarSpeedDefault = 3.0f;
        public const float CarSpawnChance = 2.0f; 
    }
}