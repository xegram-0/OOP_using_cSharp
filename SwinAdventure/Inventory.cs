
namespace SwinAdventure;

public class Inventory : Item
{
    private List<Item> _items;

    public Inventory(string[] idents, string name, string description) 
        : base(idents, name, description)
    {
        _items = new List<Item>();
        /*
        foreach (Item item in _items)
        {
            _items.Add(item);
        }
        */
    }

    public bool HasItem(string id)
    {
        return _items.Any(item => item.AreYou(id));
    }

    public void Put(Item itm)
    {
        _items.Add(itm);
    }

    public Item Take(string id)
    {
        Item item = _items.FirstOrDefault(i => i.AreYou(id));
        if (item != null)
        {
            _items.Remove(item);
        }
        return item;
    }

    public Item Fetch(string id)
    {
        return _items.FirstOrDefault(i => i.AreYou(id));
    }

    public string ItemList
    {
        get => string.Join("\n", _items.Select(i => i.Name));
    }
}
/*
 * You SET THE FREAKING NAME WITH THE S INSTEAD OF NON-S
 * WHICH LEADS TO THE INCONSISTENT WITH THE UML
*/
