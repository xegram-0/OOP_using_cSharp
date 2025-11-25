using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace FreewayClone
{
    public class Road
    {
        public List<Point2D> Points { get; private set; }
        
        public TrafficNode StartNode { get; private set; }
        public TrafficNode EndNode { get; private set; }
        
        public double TotalLength { get; private set; }
        
        public int Level { get; private set; }
        
        private const double RoadWidth = 30.0; 

        public Road(TrafficNode start, int level = 0)
        {
            Points = new List<Point2D>();
            StartNode = start;
            Level = level;
            TotalLength = 0;
            
            AddPoint(start.GetOutputPoint());
        }

        public void AddPoint(Point2D pt)
        {
            if (Points.Count > 0)
            {
                Point2D last = Points[Points.Count - 1];
                double dist = SplashKit.PointPointDistance(last, pt);
                if (dist < 5) return; 
                TotalLength += dist;
            }
            Points.Add(pt);
        }

        public void Finish(TrafficNode end)
        {
            EndNode = end;

            AddPoint(end.GetInputPoint());
        }

        public Point2D GetPositionAtDistance(double dist, double offset)
        {
            if (Points.Count < 2) return new Point2D();
            
            if (dist <= 0) dist = 0;
            if (dist >= TotalLength) dist = TotalLength;

            double currentDist = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                double segLen = SplashKit.PointPointDistance(Points[i], Points[i + 1]);
                if (currentDist + segLen >= dist || i == Points.Count - 2)
                {
                    double remaining = dist - currentDist;
                    if (remaining > segLen) remaining = segLen;
                    double ratio = remaining / segLen;
                    
                    double dx = Points[i + 1].X - Points[i].X;
                    double dy = Points[i + 1].Y - Points[i].Y;

                    double centerX = Points[i].X + dx * ratio;
                    double centerY = Points[i].Y + dy * ratio;

                    double len = Math.Sqrt(dx*dx + dy*dy);
                    double nx = -dy / len;
                    double ny = dx / len;

                    return new Point2D() 
                    { 
                        X = centerX + nx * offset, 
                        Y = centerY + ny * offset 
                    };
                }
                currentDist += segLen;
            }
            return Points[Points.Count - 1];
        }

        public void Draw()
        {
            if (Points.Count < 2) return;

            if (Level > 0)
            {
                double shadowOffset = Level * 5.0; 
                Color shadowColor = SplashKit.RGBAColor(0, 0, 0, 0.3); 

                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Point2D p1 = new Point2D() { X = Points[i].X + shadowOffset, Y = Points[i].Y + shadowOffset };
                    Point2D p2 = new Point2D() { X = Points[i+1].X + shadowOffset, Y = Points[i+1].Y + shadowOffset };
                    
                    DrawThickSegment(shadowColor, p1, p2, RoadWidth);
                    SplashKit.FillCircle(shadowColor, p1.X, p1.Y, RoadWidth / 2.0);
                }
                Point2D last = Points[Points.Count - 1];
                SplashKit.FillCircle(shadowColor, last.X + shadowOffset, last.Y + shadowOffset, RoadWidth / 2.0);
            }

            
            Color roadColor = Color.Gray;
            if (Level == 1) roadColor = Color.LightGray;
            if (Level == 2) roadColor = Color.White;

            for (int i = 0; i < Points.Count - 1; i++)
            {
                DrawThickSegment(roadColor, Points[i], Points[i + 1], RoadWidth);
                SplashKit.FillCircle(roadColor, Points[i].X, Points[i].Y, RoadWidth / 2.0);
            }
            SplashKit.FillCircle(roadColor, Points[Points.Count - 1].X, Points[Points.Count - 1].Y, RoadWidth / 2.0);

        }
        
        private void DrawThickSegment(Color color, Point2D p1, Point2D p2, double width)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len == 0) return;

            double nx = -dy / len;
            double ny = dx / len;

            double ox = nx * (width / 2.0);
            double oy = ny * (width / 2.0);

            double x1 = p1.X + ox, y1 = p1.Y + oy; 
            double x2 = p1.X - ox, y2 = p1.Y - oy;
            double x3 = p2.X - ox, y3 = p2.Y - oy; 
            double x4 = p2.X + ox, y4 = p2.Y + oy; 
            SplashKit.FillTriangle(color, x1, y1, x2, y2, x3, y3);
            SplashKit.FillTriangle(color, x1, y1, x3, y3, x4, y4);
        }
    }
}