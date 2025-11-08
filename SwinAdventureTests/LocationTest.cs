using SwinAdventure;
namespace SwinAdventureTests;

[TestFixture]
public class LocationTest
{
    private Player _testPlayer;
    private Location _testLocation;
    private Item _swordItem;
    [SetUp]
    public void Setup()
    {
        _testPlayer = new Player("Moona", "A wild Moona Pokemon");
        _testLocation = new Location("New Island", "An island of hope");
        _swordItem = new Item(["sword"], "Normal sword", "A normal sword made of steel");
    }

    [Test]
    public void TestPlayerHasLocation()
    {
        _testPlayer.Location = _testLocation;
        Assert.That(_testPlayer.Location.Name, Is.EqualTo("New Island"));
    }

    [Test]
    public void TestPlayerHasWrongLocation()
    {
        _testPlayer.Location = _testLocation;
        Assert.That(_testPlayer.Location.Name, Is.Not.EqualTo("New City"));
    }

    [Test]
    public void TestPlayerLocate()
    {
        _testPlayer.Location = _testLocation;
        Assert.That(_testPlayer.Locate("New Island"), Is.EqualTo(_testLocation));
    }

    [Test]
    public void TestItemLocation()
    {
        _testLocation.Inventory.Put(_swordItem);
        Assert.That(_swordItem, Is.EqualTo(_testLocation.Locate("sword")));
    }
    [Test]
    public void LocationDescription()
    {
        Assert.That(_testLocation.Description, Is.EqualTo("An island of hope"));
    }
    [Test]
    public void LocationFullDescription()
    {
        Assert.That(_testLocation.FullDescription, Is.EqualTo("You are in New Island. Here, you can see: A normal sword made of steel"));
    }
}