using SwinAdventure;

namespace SwinAdventureTests;

[TestFixture]
public class BagTest
{
    private Bag _bag;
    private Bag _bag2;
    private Item _item1;
    private Item _item2;
    [SetUp]
    public void SetUp()
    {
        _bag = new Bag(["First bag"], "First bag", "A big bag");
        _bag2 = new Bag(["Second bag"], "Second bag", "A small bag");
        _item1 = new Item(["Healing weed"], "Healing weed", "For healing small amount of HP");
        _item2 = new Item(["Sword"], "Sword", "A normal sword");
        
    }

    [Test]
    public void TestBagLocatesItems()
    {
        _bag.Inventory().Put(_item1);
        _bag.Inventory().Put(_item2);
        Assert.That(_bag.Locate("Healing weed"), Is.Not.Null);
    }
    [Test]
    public void TestBagLocatesItself()
    {
        Assert.That(_bag.Locate("First bag"), Is.Not.Null);
    }
    [Test]
    public void TestBagLocatesNothing()
    {
        Assert.That(_bag.Locate("Shield"), Is.Null);
    }
    [Test]
    public void TestBagFullDescription()
    {
        Assert.That(_bag.FullDescription, Is.EqualTo($"In the {_bag.Name} you can se: \n {_bag.Inventory().ItemList}"));
    }
    [Test]
    public void BagInBag()
    {
        _bag.Inventory().Put(_bag2);
        _bag.Inventory().Put(_item1);
        _bag2.Inventory().Put(_item2);
        Assert.That(_bag.Locate("Second bag"), Is.Not.Null);
        Assert.That(_bag.Locate("Healing weed"), Is.Not.Null);
        Assert.That(_bag.Locate("Sword"), Is.Null);
    }
    [Test]
    public void TestBagInBagwithPrivilegedItem()
    {
        _bag.Inventory().Put(_bag2);
        _bag2.PrivilegeEscalation("Healing weed");
        Assert.That(_bag.Locate("Healing weed"), Is.Null);
    }
}