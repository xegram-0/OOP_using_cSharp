using System.Globalization;
using System.Diagnostics;
using System.Threading;
namespace ClockApp;

public class Program
{
    private static void RunClock(int seconds)
    {
        Console.WriteLine("Clock started");
        Clock clock = new Clock();
        for (int i = 0; i < seconds; i++)
        {
            Console.WriteLine($"\rCurrent Time: {clock.Time}");
            clock.Tick();
            Thread.Sleep(1000);
        }
        Console.WriteLine("Clock ended");

    }

    private const int TICK_BENCHMARK = 10;

    public static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Process currentProcess = Process.GetCurrentProcess();
        
        GC.Collect();
        currentProcess.Refresh();
        
        Stopwatch stopwatch = new Stopwatch();
        Console.WriteLine("Benchmark started");
        stopwatch.Start();
        RunClock(TICK_BENCHMARK);
        stopwatch.Stop();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        currentProcess.Refresh();
        
        
        double elapsedTime = (double)stopwatch.ElapsedTicks * 1000 / Stopwatch.Frequency;
        Console.WriteLine($"Elapsed time: {elapsedTime:N0} ms");
        double currentMemoryInKB = currentProcess.WorkingSet64 / 1024.0;
        double peakMemoryInKB = currentProcess.PeakWorkingSet64 / 1024.0;
        Console.WriteLine($"Current physical time: {currentMemoryInKB:N0} KiB");
        Console.WriteLine($"Peak physical memory usage: {peakMemoryInKB:N0} KiB");
    }
}