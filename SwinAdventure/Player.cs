namespace SwinAdventure;

public class Player:GameObj
{
    private Inventory _inventory;

    public Player(string name, string description) : base(new string[] { "me", "inventory" }, name, description)
    {
        _inventory = new Inventory(new string[]{"inventory"}, "inventory", "Player's inventory");
    } 
     public Inventory Inventory{ get =>  _inventory; }

     public GameObj Locate(string id)
     {
         if (AreYou(id))
         {
             return this;
         }
         else
         {
             return Inventory.Fetch(id);
         }
     }

     public override string FullDescription
     {
         get { return $"You are {Name} {base.FullDescription}\n" + "You are carrying: \n" + _inventory.ItemList; }
     }

     public override void SaveTo(StreamWriter writer)
     {
         base.SaveTo(writer);
         writer.WriteLine(_inventory.ItemList);
     }

     public override void LoadFrom(StreamReader reader)
     {
         base.LoadFrom(reader);
         string ItemDescriptionList = reader.ReadLine();
         Console.WriteLine("Player information");
         Console.WriteLine(Name);
         Console.WriteLine(ShortDescription);
         Console.WriteLine(ItemDescriptionList);
     }
}