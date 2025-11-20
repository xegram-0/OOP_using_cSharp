namespace SwinAdventure;
public class Path:GameObj //This is an object for the game
{
    private Location _fromDestination;
    private Location _toDestination;
    private bool _isBlocked;
    public Path(string[] ids, string name, string description, Location fromDestination, Location toDestination) :
        base(ids, name, description)
    {
        _fromDestination = fromDestination;
        _toDestination = toDestination;
        _isBlocked =  false;
    }
    public bool IsBlocked
    {
        get => _isBlocked;
        set => _isBlocked = value;
    }
    public Location Source => _fromDestination;
    public Location Destination => _toDestination;
}