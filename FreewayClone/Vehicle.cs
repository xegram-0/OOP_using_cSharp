using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace FreewayClone
{
    public class Vehicle
    {
        private Road _currentRoad;
        private double _distanceTraveled;
        
        // Physics Properties
        private double _currentSpeed;
        private const double MaxSpeed = 4.0;
        private const double Acceleration = 0.1;
        private const double Deceleration = 0.2; 
        private double _rotation; 

        public bool HasArrived { get; private set; } = false;
        public bool HasCrashed { get; private set; } = false; 
        public Color CarColor { get; private set; }
        public Road CurrentRoad => _currentRoad;
        public TrafficNode Destination { get; private set; }
        
        public Point2D Position { get; private set; } 

        // Traffic Speed Control
        private double _trafficLimitSpeed = MaxSpeed; 

        public Vehicle(Road road, Color color, TrafficNode destination)
        {
            _currentRoad = road;
            CarColor = color;
            Destination = destination;
            _currentSpeed = 0; 
            _distanceTraveled = 0;
            _rotation = 0;
            Position = _currentRoad.GetPositionAtDistance(0, 0);
        }

        public void Update(List<Vehicle> allVehicles)
        {
            if (HasArrived || HasCrashed) return;

            // 1. Check Traffic Ahead (Cone of Vision)
            _trafficLimitSpeed = GetTrafficSpeedLimit(allVehicles);

            // 2. Calculate Desired Speed (Road constraints)
            double distRemaining = _currentRoad.TotalLength - _distanceTraveled;
            double desiredSpeed = MaxSpeed;

            if (distRemaining < 50) 
            {
                desiredSpeed = 2.0; 
            }

            // The target speed is the LOWEST of: Road Limit vs Traffic Limit
            double targetSpeed = Math.Min(desiredSpeed, _trafficLimitSpeed);

            // 3. Apply Acceleration/Deceleration
            if (_currentSpeed < targetSpeed)
            {
                _currentSpeed += Acceleration;
                if (_currentSpeed > targetSpeed) _currentSpeed = targetSpeed;
            }
            else if (_currentSpeed > targetSpeed)
            {
                // Brake harder if we are way over target
                double brakeForce = (_currentSpeed - targetSpeed > 1.5) ? Deceleration * 2 : Deceleration;
                _currentSpeed -= brakeForce;
                if (_currentSpeed < targetSpeed) _currentSpeed = targetSpeed;
            }
            
            if (_currentSpeed < 0.05) _currentSpeed = 0;

            // 4. Move
            _distanceTraveled += _currentSpeed;

            // 5. Check Arrival
            if (_distanceTraveled >= _currentRoad.TotalLength)
            {
                HasArrived = true;
                _distanceTraveled = _currentRoad.TotalLength;
            }

            // 6. Update Position
            Position = _currentRoad.GetPositionAtDistance(_distanceTraveled, 0);
            UpdateRotation();
        }

        private double GetTrafficSpeedLimit(List<Vehicle> allVehicles)
        {
            double scanDistance = 60.0; 
            double safeDistance = 25.0; 
            double minSpeed = MaxSpeed; 

            Point2D nextPos = _currentRoad.GetPositionAtDistance(_distanceTraveled + 5, 0);
            double myDx = nextPos.X - Position.X;
            double myDy = nextPos.Y - Position.Y;
            double len = Math.Sqrt(myDx*myDx + myDy*myDy);
            
            if (len > 0) { myDx /= len; myDy /= len; }
            else 
            {
                double rad = _rotation * (Math.PI / 180.0);
                myDx = Math.Cos(rad);
                myDy = Math.Sin(rad);
            }

            foreach (var other in allVehicles)
            {
                if (other == this || other.HasArrived) continue;
                if (other.CurrentRoad.Level != this.CurrentRoad.Level) continue;

                double toOtherX = other.Position.X - this.Position.X;
                double toOtherY = other.Position.Y - this.Position.Y;
                double dist = Math.Sqrt(toOtherX*toOtherX + toOtherY*toOtherY);

                if (dist < scanDistance)
                {
                    double toOtherDirX = toOtherX / dist;
                    double toOtherDirY = toOtherY / dist;
                    double dot = myDx * toOtherDirX + myDy * toOtherDirY;

                    if (dot > 0.7) 
                    {
                        if (dist < safeDistance) return 0;

                        double factor = (dist - safeDistance) / (scanDistance - safeDistance);
                        double speedLimit = MaxSpeed * factor;
                        if (speedLimit < minSpeed) minSpeed = speedLimit;
                    }
                }
            }
            return minSpeed;
        }

        private void UpdateRotation()
        {
            Point2D nextPos = _currentRoad.GetPositionAtDistance(_distanceTraveled + 5, 0);
            double dx = nextPos.X - Position.X;
            double dy = nextPos.Y - Position.Y;
            
            if (Math.Abs(dx) > 0.1 || Math.Abs(dy) > 0.1)
            {
                double angleRad = Math.Atan2(dy, dx);
                _rotation = angleRad * (180.0 / Math.PI);
            }
        }

        public bool CheckCollision(Vehicle other)
        {
            if (this == other) return false;
            if (HasCrashed || other.HasCrashed) return false;
            if (_currentRoad.Level != other.CurrentRoad.Level) return false;

            if (SplashKit.PointPointDistance(Position, other.Position) < 12)
            {
                HasCrashed = true;
                return true;
            }
            return false;
        }

        public void Crash()
        {
            HasCrashed = true;
            _currentSpeed = 0;
        }

        public TrafficNode GetArrivalNode()
        {
            return _currentRoad.EndNode;
        }

        public void TransferToRoad(Road nextRoad)
        {
            _currentRoad = nextRoad;
            _distanceTraveled = 0;
            HasArrived = false;
        }

        public void Draw()
        {
            Color drawColor = HasCrashed ? Color.OrangeRed : CarColor;
            
            // FIX: Manually brighten color for brake lights
            if (_currentSpeed < 0.5 && !HasCrashed) 
            {
                drawColor = BrightenColor(CarColor, 50); // Add 50 to RGB channels
            }

            if (Destination.Id == "55") 
            {
                SplashKit.FillRectangle(drawColor, Position.X - 6, Position.Y - 6, 12, 12);
                SplashKit.DrawRectangle(Color.Black, Position.X - 6, Position.Y - 6, 12, 12);
            }
            else if (Destination.Id == "44") 
            {
                SplashKit.FillCircle(drawColor, Position.X, Position.Y, 6);
                SplashKit.DrawCircle(Color.Black, Position.X, Position.Y, 6);
            }
            else if (Destination.Id == "33") 
            {
                Point2D p1 = GetRotatedPoint(Position, 8, _rotation);
                Point2D p2 = GetRotatedPoint(Position, 8, _rotation + 135);
                Point2D p3 = GetRotatedPoint(Position, 8, _rotation - 135);
                SplashKit.FillTriangle(drawColor, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);
                SplashKit.DrawTriangle(Color.Black, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);
            }
            else
            {
                SplashKit.FillCircle(drawColor, Position.X, Position.Y, 6);
            }
        }

        // Helper to brighten color manually
        private Color BrightenColor(Color c, double amount)
        {
            return SplashKit.RGBAColor(
                Math.Min(1.0, c.R + amount/255.0),
                Math.Min(1.0, c.G + amount/255.0),
                Math.Min(1.0, c.B + amount/255.0),
                c.A
            );
        }

        private Point2D GetRotatedPoint(Point2D center, double radius, double angleDeg)
        {
            double rad = angleDeg * (Math.PI / 180.0);
            return new Point2D()
            {
                X = center.X + radius * Math.Cos(rad),
                Y = center.Y + radius * Math.Sin(rad)
            };
        }
    }
}