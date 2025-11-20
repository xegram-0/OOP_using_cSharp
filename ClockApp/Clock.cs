namespace ClockApp;

public class Clock
{
    private readonly Counter _seconds;
    private readonly Counter _minutes;
    private readonly Counter _hours;

    public Clock()
    {
        _hours = new Counter("Hours");
        _minutes = new Counter("Minutes");
        _seconds = new Counter("Seconds");
    }

    public void Tick()
    {
        _seconds.Increment();
        if (_seconds.Tick != 60)
        {
            return;
        }
        _seconds.Reset();
        _minutes.Increment();
        if (_minutes.Tick != 60)
        {
            return;
        }
        _minutes.Reset();
        _hours.Increment();
        if (_hours.Tick != 24)
        {
            return;
        }
        _hours.Reset();
    }

    public void Reset()
    {
        _seconds.Reset();
        _minutes.Reset();
        _hours.Reset();
    }

    public string Time => $"{_hours.Tick:D2}:{_minutes.Tick:D2}:{_seconds.Tick:D2}";
}