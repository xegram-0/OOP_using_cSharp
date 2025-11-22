using System.Collections.Generic;
using SplashKitSDK;

namespace FreewaysGame
{
    public class Road
    {
        public List<Point2D> PathPoints { get; private set; }
        private List<Triangle> _renderTriangles;
        private List<Circle> _joints; // Circles to smooth the corners

        public Road(List<Point2D> points)
        {
            PathPoints = new List<Point2D>(points);
            _renderTriangles = new List<Triangle>();
            _joints = new List<Circle>();
            
            BakeGeometry();
        }

        /// <summary>
        /// Converts the raw line points into a wide road mesh (Triangle Strip).
        /// </summary>
        private void BakeGeometry()
        {
            _renderTriangles.Clear();
            _joints.Clear();

            if (PathPoints.Count < 2) return;

            for (int i = 0; i < PathPoints.Count - 1; i++)
            {
                Point2D p1 = PathPoints[i];
                Point2D p2 = PathPoints[i + 1];

                // 1. Get direction and normal
                Vector2D dir = SplashKit.VectorPointToPoint(p1, p2);
                dir = Utilities.SafeUnitVector(dir);
                
                // Skip invalid segments
                if (SplashKit.VectorMagnitude(dir) == 0) continue;

                Vector2D normal = Utilities.GetNormal(dir);
                Vector2D widthOffset = SplashKit.VectorMultiply(normal, GameConfig.RoadWidth / 2);

                // 2. Calculate the 4 corners of this road segment
                Point2D currentLeft = Utilities.AddVectorToPoint(p1, widthOffset);
                Point2D currentRight = Utilities.AddVectorToPoint(p1, SplashKit.VectorInvert(widthOffset));
                Point2D nextLeft = Utilities.AddVectorToPoint(p2, widthOffset);
                Point2D nextRight = Utilities.AddVectorToPoint(p2, SplashKit.VectorInvert(widthOffset));

                // 3. Create 2 Triangles to make a rectangle
                _renderTriangles.Add(SplashKit.TriangleFrom(currentLeft, currentRight, nextRight));
                _renderTriangles.Add(SplashKit.TriangleFrom(currentLeft, nextRight, nextLeft));

                // 4. Create a joint circle at p1 to smooth the connection
                _joints.Add(SplashKit.CircleAt(p1, GameConfig.RoadWidth / 2));
            }
            
            // Add final joint at the very end
            if (PathPoints.Count > 0)
            {
                _joints.Add(SplashKit.CircleAt(PathPoints[PathPoints.Count-1], GameConfig.RoadWidth / 2));
            }
        }

        public void Draw()
        {
            // Draw the main asphalt (Rectangles)
            foreach (var tri in _renderTriangles)
            {
                SplashKit.FillTriangle(GameConfig.RoadColor, tri);
            }

            // Draw the joints (Circles) to fill gaps in sharp turns
            foreach (var circle in _joints)
            {
                SplashKit.FillCircle(GameConfig.RoadColor, circle);
            }
        }
    }
}