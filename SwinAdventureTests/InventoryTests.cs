
using SwinAdventure;

namespace SwinAdventureTests;

[TestFixture]
public class InventoryTests
{
    private Inventory  _inventory;
    private Item _sword;
    [SetUp]
    public void Setup()
    {
        _inventory = new Inventory(new[] { "bag","BAG"} , "Bag", "A BIG BAG");
        _sword = new Item(new[]{"sword", "SWORD"}, "Sword", "A LONG SWORD");
    }

    [Test]
    public void TestFindItem()
    {
        _inventory.Put(_sword);
        Assert.That(_inventory.HasItem("sword"));
    }
    [Test]
    public void TestNoItemFind()
    {
        _inventory.Put(_sword);
        Assert.That(_inventory.HasItem("mace"), Is.False);
    }
    [Test]
    public void TestFetchItem()
    {
        _inventory.Put(_sword);
        Item fetched = _inventory.Fetch("sword");
        Assert.That(fetched, Is.Not.Null);
        Assert.That(fetched.FirstId, Is.EqualTo("sword")); //Check the name and the id for this one
    }
    [Test]
    public void TestTakeItem()
    {
        _inventory.Put(_sword);
        Assert.That(_inventory.HasItem("sword"));
        Item taken = _inventory.Take("sword");
        Assert.That(_inventory.HasItem("sword"), Is.False);
    }
    [Test]
    public void TestItemList()
    {
        Item bottle = new Item(new[]{"bottle"}, "Bottle", "A full bottle of water");
        Item bread = new Item(new[]{"bread"}, "Bread", "A delicious bread");
        
        _inventory.Put(bottle);
        _inventory.Put(bread);

        string itemList = _inventory.ItemList;
        
        Assert.That(itemList.Contains("Bottle"), Is.True);
        Assert.That(itemList.Contains("Bread"), Is.True);
        //ItemList takes the name not the other things if this is confusing
    }

    [Test]
    public void TestRemoveItem()
    {
        Item bottle = new Item(new[]{"bottle"}, "Bottle", "A full bottle of water");
        Item bread = new Item(new[]{"bread"}, "Bread", "A delicious bread");
        
        _inventory.Put(bottle);
        _inventory.Put(bread);
        
        _inventory.RemoveItem(bottle);
        Assert.That(_inventory.HasItem("bottle"), Is.False);
        Assert.That(_inventory.HasItem("bread"), Is.True);
    }
}
