using System.ComponentModel.Design.Serialization;

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
    public string ShortDescription => $"{_name}";
    public virtual string FullDescription => Description; //Not description
    public virtual void SaveTo(StreamWriter writer)
    {
        writer.WriteLine(_name);
        writer.WriteLine(_description);
    }
    public virtual void LoadFrom(StreamReader reader)
    {
        _name = reader.ReadLine() ?? string.Empty;
        _description = reader.ReadLine() ?? string.Empty;
    }
}