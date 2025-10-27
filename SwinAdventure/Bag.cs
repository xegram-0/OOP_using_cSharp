namespace SwinAdventure;

public class Bag :Item, IHaveInventory
{
    private Inventory _inventory;

    public Bag(string[] ids, string name, string description) : base(ids, name, description)
    {
        _inventory = new Inventory();
    }

    public GameObj? Locate(string id)
    {
        if (AreYou(id))
        {
            return this;
        }
        if (_inventory.HasItem(id))
        {
            return _inventory.Fetch(id);
        }
        return null;
    }
    public Inventory Inventory() => _inventory;
    public override string FullDescription => $"In the {Name} you can see: \n {_inventory.ItemList}";
}