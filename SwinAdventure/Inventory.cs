namespace SwinAdventure;

public class Inventory : Items
{
    private List<Items> _items;

    public Inventory()
    {
        _items = new List<Items>();
        foreach (Items item in _items)
        {
            _items.Add(item);
        }
    }

    public bool HasItem(string id)
    {
        bool result = AreYou(id);
        return result;
    }

    public void Put(Items itm)
    {
        _items.Add(itm);
    }

    public Item Take(string id)
    {
        _items.Remove(id);
    }

    public Item Fetch(string id)
    {
        if (AreYou(id))
        {
            return _items[id];
        }
    }

    public string ItemList
    {
        get => _items;
        set => _items = value;
    }
}