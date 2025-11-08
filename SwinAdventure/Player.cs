namespace SwinAdventure;
public class Player:GameObj, IHaveInventory
{
    private readonly Inventory _inventory;
    private Location _location;

    public Location Location
    {
        get => _location;
        set => _location = value;
    }
    public Player(string name, string description) : base(["me", "inventory" ], name, description) // ID is set to "me" and "inventory"
    {
        _inventory = new Inventory();
        
    } 
     public Inventory Inventory => _inventory;
     public GameObj? Locate(string id)
     {
         /*
          * Give ID, if ID is about you, return you - I am here.
          * If it is not you, then ID is about the object you may have
          * Returns the object or null if you do not have it
          */
         if (AreYou(id))
         {
             return this; // Return the instance of the current obj
         }

         GameObj item = _inventory.Fetch(id);
         if (item != null){ return item; }
         if (_location != null){ return _location; }
         return null;
     }
     public override string FullDescription => $"You are {Name} - {base.FullDescription}\n" + 
                                               "You are carrying: " + _inventory.ItemList;
     public override void SaveTo(StreamWriter writer)
     {
         base.SaveTo(writer);
         writer.WriteLine(_inventory.ItemList);
         
         //Verification
         writer.WriteLine("Created by NHN");
         writer.WriteLine("ID: isSomething");

     }
     public override void LoadFrom(StreamReader reader)
     {
         base.LoadFrom(reader);
         string itemDescriptionList = reader.ReadLine()?? string.Empty;
         /*
          * ?? is asking if the reader reaches the end of the line, it gives "" - an empty string instead of null.
          * This to avoid NullReferenceException if itemDescriptionList uses Trim() or something about the string
          * by guaranteeing it is never null
          */
         Console.WriteLine("Player information");
         Console.WriteLine(Name);
         Console.WriteLine(ShortDescription); // Return $"{_name} {FirstId}"; thus James me
         Console.WriteLine(itemDescriptionList);
         
         //Verification
         string creatorString = reader.ReadLine() +"\n"
                                + reader.ReadLine();
         Console.WriteLine(creatorString);
     }
}