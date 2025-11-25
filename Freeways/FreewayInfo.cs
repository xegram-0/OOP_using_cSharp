using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics; // Added for Process lookup


namespace Freeways
{
    public class FreewayInfo
    {
        public IntPtr Hwnd { get; private set; }
        public NativeMethods.RECT Bounds { get; private set; }
        public bool[] ButtStat { get; set; } = { false, false };

        // Cache needed for math references
        public System.Collections.Generic.Dictionary<string, LineSegment> LineTable = new();
        public System.Collections.Generic.Dictionary<string, LineEq> EquationTable = new();

        public FreewayInfo()
        {
            InitWindow();
        }

        private void InitWindow()
        {
            // Strategy 1: Find by specific Window Title "Freeways"
            Hwnd = NativeMethods.FindWindow(null, "Freeways");

            // Strategy 2: If not found, find by Process Name "Freeways"
            if (Hwnd == IntPtr.Zero)
            {
                Process[] processes = Process.GetProcessesByName("Freeways");
                if (processes.Length > 0)
                {
                    Hwnd = processes[0].MainWindowHandle;
                }
            }

            if (Hwnd == IntPtr.Zero)
            {
                throw new Exception("Freeways game window not found! Is the game running?");
            }

            NativeMethods.SetForegroundWindow(Hwnd);
            Thread.Sleep(500); // Wait for focus

            if (NativeMethods.GetWindowRect(Hwnd, out NativeMethods.RECT rect))
            {
                // Adjust for borders roughly as Python script did
                // Note: These offsets might need tweaking depending on your OS (Win 10 vs 11)
                rect.Left += 8;
                rect.Top += 32;
                rect.Right -= 8; 
                rect.Bottom -= 8; 
                this.Bounds = rect;
            }
            else
            {
                throw new Exception("Could not get window bounds.");
            }
        }

        public Bitmap CaptureScreen()
        {
            int width = Bounds.Right - Bounds.Left;
            int height = Bounds.Bottom - Bounds.Top;
            
            // Safety check
            if (width <= 0 || height <= 0) return new Bitmap(1, 1);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(Bounds.Left, Bounds.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }

        public void ClearLevel()
        {
            // Menu button heuristic location based on Python script
            // 140 shift for clear
            FindMenu(140, true); 
        }

        private void FindMenu(int shiftSize, bool pageSwitch)
        {
            // Basic implementation: clicks top right area
            int menuY = Bounds.Bottom - 40;
            int menuXBase = Bounds.Left + 34; // Defaulting to first chunk
            
            int clickX = menuXBase + shiftSize;
            InputSimulator.ClickLeft(clickX, menuY);
        }
    }

    public struct LineSegment
    {
        public PointD From;
        public PointD To;
    }
}