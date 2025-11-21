using SplashKitSDK;
namespace FreewaysClone;

public static class Util
{
    // Helper to replace the missing SplashKit function
    public static Point2D AddVectorToPoint(Point2D p, Vector2D v)
    {
        return new Point2D() { X = p.X + v.X, Y = p.Y + v.Y };
    }
}