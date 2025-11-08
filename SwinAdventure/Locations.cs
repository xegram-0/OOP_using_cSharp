namespace SwinAdventure;

public class Locations(string []ids, string name, string description) : GameObj(ids, name, description), IHaveInventory
{
    private readonly Inventory _inventory = new Inventory();
    public GameObj Locate(string id)
    {
        if (AreYou(id))
        {
            return _inventory;
        }
        return Inventory.Fetch(id);
    }

    public override string FullDescription
    {
        get
        {
            string nameDescription;
            string inventoryDescription;
            if (name != null && Name != "")
            {
                nameDescription = name;
            }
            else
            {
                nameDescription = "An unknown location";
            }

            if (_inventory != null && _inventory.ItemList != null)
            {
                inventoryDescription = _inventory.ItemList;
            }
            else
            {
                inventoryDescription = "There are no items at this location.";
            }

            return "You are in " + nameDescription + ". " +
                   base.FullDescription +
                   "\n Here, you can see: " + inventoryDescription;;
        }
    }
    private Inventory Inventory => _inventory;
}