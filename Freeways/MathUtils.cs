using System;
using System.Collections.Generic;

namespace Freeways
{
    public struct PointD { public double X; public double Y; }
    public struct LineEq { public double Slope; public double YIntercept; }

    public static class MathUtils
    {
        public const double BIGNUM = 100000000000.0;

        public static PointD NumToLocation(int mapNo)
        {
            if (mapNo > 80 || mapNo < 0) return new PointD { X = -1, Y = -1 };
            if (mapNo == 0) return new PointD { X = 4, Y = 3 };
            
            if (mapNo < 4) mapNo -= 1;
            
            int diag = 1;
            while (diag * diag <= mapNo) diag += 1;
            diag -= 1;

            int lastCrn, xoff, yoff;

            if (diag % 2 == 0) lastCrn = 4 - diag / 2;
            else lastCrn = 4 + diag / 2;

            int xtra = mapNo - diag * diag;
            if (xtra < diag)
            {
                xoff = 0;
                yoff = xtra;
            }
            else
            {
                yoff = diag;
                xoff = xtra - diag;
            }

            if (diag % 2 == 0) return new PointD { X = lastCrn + xoff, Y = lastCrn + yoff };
            else return new PointD { X = lastCrn - xoff + 1, Y = lastCrn - yoff };
        }

        public static LineEq GetEquation(PointD p1, PointD p2)
        {
            double diffx = p2.X - p1.X;
            double diffy = p2.Y - p1.Y;
            if (Math.Abs(diffx) < 0.0001) return new LineEq { Slope = BIGNUM, YIntercept = p1.X };
            
            double slope = diffy / diffx;
            double yInt = p1.Y - slope * p1.X;
            return new LineEq { Slope = slope, YIntercept = yInt };
        }

        public static LineEq GetPerpendicular(LineEq eq, PointD point)
        {
            if (eq.Slope == 0) return new LineEq { Slope = BIGNUM, YIntercept = point.X };
            double slope = -1 / eq.Slope;
            double yInt = point.Y - point.X * slope;
            if (Math.Abs(slope) < 1e-10) slope = 0;
            return new LineEq { Slope = slope, YIntercept = yInt };
        }

        public static PointD GetIntersection(LineEq eq1, LineEq eq2)
        {
            if (eq1.Slope == BIGNUM) return new PointD { X = eq1.YIntercept, Y = eq1.YIntercept * eq2.Slope + eq2.YIntercept };
            if (eq2.Slope == BIGNUM) return new PointD { X = eq2.YIntercept, Y = eq2.YIntercept * eq1.Slope + eq1.YIntercept };

            double num = eq2.YIntercept - eq1.YIntercept;
            double den = eq1.Slope - eq2.Slope;
            double x = num / den;
            double y = eq1.Slope * x + eq1.YIntercept;
            return new PointD { X = x, Y = y };
        }

        public static int[] GetDirection(LineEq thisEq, LineEq otherEq, PointD otherTo, double otherToY)
        {
            // Simplified translation of Python get_direction logic
            // Note: In Python code, it checks against bval and other_line coordinates
            int[] ret = { -1, -1 };
            double m = thisEq.Slope;
            double b = thisEq.YIntercept;

            if (m == BIGNUM)
            {
                // Assuming other_line 'from' X < bval logic from python
                // Simplified: We assume the caller passes relevant context if needed
                // For direct port, we need the full line table. 
                // This function usually requires lookups in the line dictionary.
                return ret; 
            }
            
            // Logic ported strictly requires context of the whole line table.
            // See Compiler.cs for contextual usage.
            return ret; 
        }

        public static double GetRadiiAngle(double slope, PointD origin, PointD lineTo)
        {
            double angle = 0;
            if (slope == 0 || slope == BIGNUM) return 0; // Simplified base case
            
            if (origin.X > lineTo.X) angle = Math.PI;
            angle += Math.Atan(slope);
            if (angle < 0) angle += Math.PI / 2;
            
            double deg = angle * 180 / Math.PI;
            deg = (int)(deg + 0.5);
            while (deg > 0) deg -= 90;
            return deg + 90;
        }
    }
}