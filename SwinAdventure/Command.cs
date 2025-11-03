namespace SwinAdventure;

public abstract class Command(string[] ids):IdentifiableObj(ids)
{ 
    public abstract string Execute(Player p, string[] text);
}