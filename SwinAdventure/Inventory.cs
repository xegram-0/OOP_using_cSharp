
namespace SwinAdventure;

public class Inventory 
{
    private List<Item> _items;
    private string[] _idents;
    public Inventory(string[] idents, string name, string description)
    {
        _items = new List<Item>();
        _idents = idents;
    }

    public bool HasItem(string id)
    {
        return _items.Any(item => item.AreYou(id)); //Is there any items that satisfy the condition based on id
    }

    public void Put(Item itm)
    {
        _items.Add(itm);
    }

    public Item Take(string id)
    {
        Item item = _items.FirstOrDefault(i => i.AreYou(id)); //Find first instance of i that i is based on id
        if (item != null)                                           // If item is not empty, remove
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
        get => string.Join("\n", _items.Select(i => i.Name));  //Return string, that string is the name of the object items
    }

    public void RemoveItem(Item itm)
    {
        _items.Remove(itm);
    }

   public bool Put_ItemWithLimit(Item itm)
    {
        if (itm.IdentCount <= 3)
        {
            _items.Add(itm);
            return true;
        }

        return false;
    }
}
/*
 * You SET THE FREAKING NAME WITH THE S INSTEAD OF NON-S (Items)
 * WHICH LEADS TO THE INCONSISTENT WITH THE UML
*/
