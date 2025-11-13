namespace SwinAdventure;

public class Path:GameObj
{
    private Location _source;
    private Location _destination;
    private bool _isBlocked;

    public Path(string[] ids, string name, string description, Location source, Location destination) :
        base(ids, name, description)
    {
        _source = source;
        _destination = destination;
        _isBlocked =  false;
    }

    public bool IsBlocked
    {
        get => _isBlocked;
        set => _isBlocked = value;
    }
    public Location Source => _source;
    public Location Destination => _destination;
}