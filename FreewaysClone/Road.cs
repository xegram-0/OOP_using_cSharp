using SplashKitSDK;
namespace FreewaysClone;

public class Road
{
        public List<Point2D> PathPoints { get; private set; }
        private List<Triangle> _renderTriangles;
        private float _roadWidth = 30.0f;

        public Road(List<Point2D> points)
        {
            PathPoints = points;
            _renderTriangles = new List<Triangle>();
            GenerateMesh();
        }

        private void GenerateMesh()
        {
            _renderTriangles.Clear();

            // Need at least 2 points to make a segment
            if (PathPoints.Count < 2) return;

            for (int i = 0; i < PathPoints.Count - 1; i++)
            {
                Point2D p1 = PathPoints[i];
                Point2D p2 = PathPoints[i + 1];

                // 1. Calculate Direction Vector
                Vector2D dir = SplashKit.VectorPointToPoint(p1, p2);
                
                // Safety check for zero-length segments
                if (SplashKit.VectorMagnitude(dir) < 0.1) continue;
                
                dir = SplashKit.UnitVector(dir); 

                // 2. Calculate Normal (Perpendicular)
                Vector2D normal = SplashKit.VectorTo(-dir.Y, dir.X);

                // 3. Calculate Edges
                Vector2D widthOffset = SplashKit.VectorMultiply(normal, _roadWidth / 2);
                
                Point2D currentLeft = Util.AddVectorToPoint(p1, widthOffset);
                Point2D currentRight = Util.AddVectorToPoint(p1, SplashKit.VectorInvert(widthOffset));

                Point2D nextLeft = Util.AddVectorToPoint(p2, widthOffset);
                Point2D nextRight = Util.AddVectorToPoint(p2, SplashKit.VectorInvert(widthOffset));

                // 4. Create Triangles
                Triangle t1 = SplashKit.TriangleFrom(currentLeft, currentRight, nextRight);
                Triangle t2 = SplashKit.TriangleFrom(currentLeft, nextRight, nextLeft);

                _renderTriangles.Add(t1);
                _renderTriangles.Add(t2);
            }
        }

        public void Draw()
        {
            foreach (var tri in _renderTriangles)
            {
                SplashKit.FillTriangle(Color.RGBColor(80, 80, 80), tri);
            }

            foreach(var p in PathPoints)
            {
                SplashKit.FillCircle(Color.RGBColor(80, 80, 80), p, _roadWidth/2);
            }
        }
}