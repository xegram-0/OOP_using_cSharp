namespace SwinAdventure;

public class GameObj
{
    private string _name;
    private string _description;

    public GameObj(string[] ids, string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string Name
    {
        get { return _name; }
    }
    public string ShortDescription
    {
        get { return _description.Length > 5 ?  _description.Substring(0, 4) : _description; }
    }

    public virtual string FullDescription
    {
        get { return _description; }
    }
}