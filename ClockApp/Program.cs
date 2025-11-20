using System.Globalization;
using System.Diagnostics;
using System.Threading;
namespace ClockApp;

public class Program
{
    private static void RunClock(int seconds)
    {
        Clock clock = new Clock();
        for (int i = 0; i < seconds; i++)
        {
            clock.Tick();
        }
    }

    private const int TICK_BENCHMARK = 104844794;

    public static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Process currentProcess = Process.GetCurrentProcess();
        
        GC.Collect();
        currentProcess.Refresh();
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        RunClock(TICK_BENCHMARK);
        stopwatch.Stop();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        currentProcess.Refresh();
        
        
        double elapsedTime = (double)stopwatch.ElapsedTicks * 1000000 / Stopwatch.Frequency;
        Console.WriteLine($"Elapsed time: {elapsedTime:N0} microseconds");
        Console.WriteLine($"Current physical time: {currentProcess.WorkingSet64:N0} bytes");
        Console.WriteLine($"Peak physical memory usage: {currentProcess.PeakWorkingSet64:N0} bytes");
    }
}