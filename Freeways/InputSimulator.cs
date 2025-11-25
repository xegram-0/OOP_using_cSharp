using System;
using System.Threading;

namespace Freeways
{
    public static class InputSimulator
    {
        /// <summary>
        /// Moves the mouse instantly to specific coordinates.
        /// </summary>
        public static void MoveTo(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
        }

        /// <summary>
        /// Simulates a mouse drag or slow move, similar to pyautogui.moveTo(duration=1.5).
        /// The game likely relies on the drag events being polled over time.
        /// </summary>
        public static void MoveToSmooth(int startX, int startY, int endX, int endY, double durationSeconds)
        {
            int steps = (int)(durationSeconds * 60); // 60 steps per second
            if (steps <= 0) steps = 1;
            int delay = (int)(durationSeconds * 1000 / steps);

            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                // Linear interpolation
                int currentX = (int)(startX + (endX - startX) * t);
                int currentY = (int)(startY + (endY - startY) * t);

                NativeMethods.SetCursorPos(currentX, currentY);
                Thread.Sleep(delay);
            }
            // Ensure we land exactly on target
            NativeMethods.SetCursorPos(endX, endY);
        }

        public static void MouseDownLeft()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseUpLeft()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseDownRight()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void MouseUpRight()
        {
            NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void ClickLeft(int x, int y)
        {
            MoveTo(x, y);
            MouseDownLeft();
            Thread.Sleep(50);
            MouseUpLeft();
        }
    }
}