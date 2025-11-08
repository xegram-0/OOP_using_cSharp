namespace SwinAdventure;
public class LookCommand(string[] ids):Command(ids)
{
    //public LookCommand(string[] ids):base(ids){} - Convert to primary constructor
    public override string Execute(Player p, string[] text)
    {
        if (text.Length == 1 && text[0].ToLower() == "look")
        {
            return LookAtIn("Location", p); 
        }
        if(text.Length != 3 && text.Length != 5)
        {
            return "I don't know how to look like that";
        }
        if (text[0] != "look")
        {
            return "Error in look input";
        }
        if (text[1] != "at")
        {
            return "What do you want to look at?";
        }
        if (text.Length == 5 && text[3] != "in")
        {
            return "What do you want to look in?";
        }
        if (text.Length == 3)
            return LookAtIn(text[2],p);
        if (text.Length == 5)
        {
            var result = FetchContainer(p, text[4]); //don't ask me why the ide said this is good
            if (result == null)
            {
                return "I cannot find the " + text[4];
            }
            return LookAtIn(text[2], result);
        }
        return "There is an error in my program";
    }
    private IHaveInventory? FetchContainer(Player p, string containerId) //?: the compiler that the method might return null
    {
        IHaveInventory? container =  p.Locate(containerId) as IHaveInventory;
        //~container = obj as IHaveInventory;
        return container;
    }

    private string LookAtIn(string thingId, IHaveInventory container)
    {
        GameObj? item = container.Locate(thingId);
        if (item == null) { return "I cannot find the " + thingId; }
        return item.FullDescription;
    }
}