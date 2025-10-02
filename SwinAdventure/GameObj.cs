namespace SwinAdventure;

public abstract class GameObj : IdentifiableObj
{
    private string _name;
    private string _description;
    public GameObj(string[] ids, string name, string description) : base(ids)
    {
        _name = name;
        _description = description;
    }
    public string Name => _name;
    public string Description => _description;
    public string ShortDescription => $"{_name} {FirstId}";
    public virtual string FullDescription => _description;
}