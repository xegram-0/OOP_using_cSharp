namespace SwinAdventure;

public class Location(string name, string description) : GameObj(["location", name.ToLower()], name, description), IHaveInventory // This is a rabit hole but to let the test case pass associated with testplayer
{
    private readonly Inventory _inventory = new Inventory();
    public Inventory Inventory => _inventory;
    private List<Path> _path = new List<Path>();
    public GameObj Locate(string id)
    {
        if (AreYou(id))
        {
            return this;
        }
        return Inventory.Fetch(id);
    }

    public override string FullDescription
    {
        get
        {
            string nameDescription;
            string inventoryDescription;
            if (name != null && name != "")
            {
                nameDescription = name;
            }
            else
            {
                nameDescription = "An unknown location";
            }

            if (_inventory is { ItemList: {} }) // if (_inventory != null && _inventory.ItemList != null) 
            {
                inventoryDescription = _inventory.ItemList;
            }
            else
            {
                inventoryDescription = "There are no items at this location.";
            }

            return "You are in " + nameDescription + " - " +
                   base.FullDescription + ". Here, you can see: " + inventoryDescription;
        }
    }
    public void AddPath(Path path) {_path.Add(path);}

    public string PathList
    {
        get
        {
            if (_path.Count == 0)
            {
                return "No paths lead to other locations";
            }

            string path = "You can reach: ";
            for (int i = 0; i < _path.Count; i++)
            {
                path += _path[i].Name;
                if (i < _path.Count - 1)
                {
                    path += ", ";
                }
            }
            return path;
        }
            
    }
}