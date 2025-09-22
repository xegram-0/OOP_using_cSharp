using System.Security.Cryptography;

namespace CounterTask;

public class Counter
{
    private string _name;
    private int _count;
    private int _limit;

    public Counter(string name)
    {
        _name = name;
        _count = 0;
        _limit = 25;
    }

    public void Increment()
    {
        _count += 1;
        if (_count >= _limit)
        {
            Reset();
            Console.WriteLine("Reset counter.");
        }
    }

    public void Reset()
    {
        _count = 0;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Ticks
    {
        get { return _count; }
    }

    public int ResetByDefault()
    {
        if (_count == 255)
        {
            _count = 0;
        }
        return _count;
    }
}