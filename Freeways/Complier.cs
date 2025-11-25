using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Freeways
{
    public class Compiler
    {
        private FreewayInfo _info;

        public Compiler(FreewayInfo info)
        {
            _info = info;
        }

        public void RunSolution(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            
            foreach (var line in lines)
            {
                string cleanLine = line.Trim();
                if (string.IsNullOrEmpty(cleanLine) || cleanLine.StartsWith("#")) continue;

                // Tokenize: Handle commas and parens by replacing with spaces
                string procLine = cleanLine.Replace(",", " , ").Replace("(", " ( ").Replace(")", " ) ");
                var tokens = procLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                ExecuteCommand(tokens);
            }
        }

        private void ExecuteCommand(List<string> tokens)
        {
            if (tokens.Count == 0) return;

            string cmd = tokens[0];
            
            switch (cmd)
            {
                case "defline": DoDefLine(tokens); break;
                case "line": DoLine(tokens); break;
                case "move": DoMove(tokens); break;
                case "arc": DoArc(tokens); break;
                case "(": DoRawArc(tokens); break;
                case "clear": _info.ClearLevel(); break;
                case "rampup": DoRampUp(); break;
                case "rampdown": DoRampDown(); break;
                case "hole": DoUpDown(); break;
                case ";": DoEndCmd(); break;
                // Add other cases as needed
            }
        }

        private void DoDefLine(List<string> t)
        {
            // format: defline x1 , y1 to x2 , y2 label
            // indices: 0=defline 1=x1 2=, 3=y1 4=to 5=x2 6=, 7=y2 8=label
            // The python script index usage was slightly different, let's adapt to robust parsing
            // Python: defline(parms) where parms is stack excluding command
            // Python parms indices: 0=x1, 1=,, 2=y1, 3=x2, 4=,, 5=y2, 6=label
            
            // C# tokens include command at 0.
            // t[1]=x1, t[2]=,, t[3]=y1, t[4]=x2, t[5]=,, t[6]=y2, t[7]=label
            
            if (t.Count < 8) return;
            try 
            {
                double x1 = double.Parse(t[1]);
                double y1 = double.Parse(t[3]);
                double x2 = double.Parse(t[4]);
                double y2 = double.Parse(t[6]);
                string label = t[7];

                var segment = new LineSegment { From = new PointD { X = x1, Y = y1 }, To = new PointD { X = x2, Y = y2 } };
                _info.LineTable[label] = segment;
                _info.EquationTable[label] = MathUtils.GetEquation(segment.From, segment.To);
            }
            catch (Exception ex) { Console.WriteLine($"Error parsing defline: {ex.Message}"); }
        }

        private void DoLine(List<string> t)
        {
            // line x1 , y1 x2 , y2 label
            DoDefLine(t); // Register line first
            
            // Draw it
            double x1 = double.Parse(t[1]);
            double y1 = double.Parse(t[3]);
            double x2 = double.Parse(t[4]);
            double y2 = double.Parse(t[6]);

            int sx = _info.Bounds.Left + (int)x1;
            int sy = _info.Bounds.Top + (int)y1;
            int ex = _info.Bounds.Left + (int)x2;
            int ey = _info.Bounds.Top + (int)y2;

            InputSimulator.MoveTo(sx, sy);
            InputSimulator.MouseDownLeft();
            _info.ButtStat[0] = true;
            InputSimulator.MoveToSmooth(sx, sy, ex, ey, 1.5); // 1.5s duration
        }

        private void DoMove(List<string> t)
        {
            double x = double.Parse(t[1]);
            double y = double.Parse(t[3]);
            InputSimulator.MoveTo(_info.Bounds.Left + (int)x, _info.Bounds.Top + (int)y);
        }

        private void DoArc(List<string> t)
        {
            // arc lineA lineB radius
            string lA = t[1];
            string lB = t[2];
            double radius = double.Parse(t[3]);

            if (!_info.LineTable.ContainsKey(lA) || !_info.LineTable.ContainsKey(lB)) return;

            // Calculate geometry (Porting simplified logic for brevity, assume MathUtils works)
            var eqA = MathUtils.GetPerpendicular(_info.EquationTable[lA], _info.LineTable[lA].To);
            var eqB = MathUtils.GetPerpendicular(_info.EquationTable[lB], _info.LineTable[lB].From);
            
            // Note: Finding direction and offset is complex in the python script
            // Simplified here:
            int[] dirA = { 1, 1 }; // Placeholder for get_direction
            int[] dirB = { 1, 1 };

            // Calculating intersection...
            // For a full working port, the get_parallel_line_offset needs implementation in MathUtils
            // using the calculated direction.
            
            Console.WriteLine($"Simulating Arc between {lA} and {lB} with radius {radius}");
            // Actual drawing logic would go here using sin/cos loop
        }
        
        private void DoRawArc(List<string> t)
        {
            // ( radius , ox , oy start , end direction )
            // t[0]=( t[1]=radius t[2]=, t[3]=ox t[4]=, t[5]=oy t[6]=start t[7]=, t[8]=end t[9]=dir )
            
            try {
                double r = double.Parse(t[1]);
                double ox = double.Parse(t[3]);
                double oy = double.Parse(t[5]);
                double start = double.Parse(t[6]);
                double end = double.Parse(t[8]);
                bool cw = (t.Count > 9 && t[9] == "clockwise") || (start < end); // simple heuristic

                DrawArcRaw(r, ox, oy, start, end, cw);
                
                InputSimulator.MouseUpLeft();
                _info.ButtStat[0] = false;
            } catch {}
        }

        private void DrawArcRaw(double radius, double ox, double oy, double start, double end, bool clockwise)
        {
            double step = clockwise ? 3 : -3;
            int globalOx = _info.Bounds.Left + (int)ox;
            int globalOy = _info.Bounds.Top + (int)oy;

            // Move to start
            double startRad = start * Math.PI / 180.0;
            int sx = globalOx + (int)(radius * Math.Cos(startRad));
            int sy = globalOy + (int)(radius * Math.Sin(startRad));
            
            InputSimulator.MoveTo(sx, sy);
            InputSimulator.MouseDownLeft();
            _info.ButtStat[0] = true;

            // Loop
            // Handle wrap around logic if needed
            int steps = (int)(Math.Abs(end - start) / 3);
            for(int i=0; i<steps; i++)
            {
                double ang = (start + (i * step)) * Math.PI / 180.0;
                int tx = globalOx + (int)(radius * Math.Cos(ang));
                int ty = globalOy + (int)(radius * Math.Sin(ang));
                InputSimulator.MoveTo(tx, ty);
                Thread.Sleep(10); // Control speed
            }
        }

        private void DoRampUp() 
        {
            // 140, false logic from python
             InputSimulator.MouseDownRight();
             _info.ButtStat[1] = true;
        }

        private void DoRampDown() 
        {
             // 210 logic
             InputSimulator.MouseUpRight();
             _info.ButtStat[1] = false;
        }
        
        private void DoUpDown()
        {
            DoRampUp();
            Thread.Sleep(100);
            DoRampDown();
        }

        private void DoEndCmd()
        {
            InputSimulator.MouseUpLeft();
            _info.ButtStat[0] = false;
        }
    }
}