using SwinAdventure;

namespace SwinAdventureTests;

[TestFixture]
public class PlayerTest
{
    private Player _testPlayer;
    [SetUp]
    public void SetUp()
    {
        _testPlayer = new Player("James", "an explorer");
        Item item1 = new Item([ "silver", "hat" ], "A silver hat", "A very shiny silver hat");
        Item item2 = new Item([ "light", "torch"], "a torch", "a torch to light the path");
        _testPlayer.Inventory.Put(item1);
        _testPlayer.Inventory.Put(item2);
        Location _testLocation = new Location("New York", "A city of liberty");
        _testPlayer.Location = _testLocation;
    }

    [Test]
    public void IdentifiablePlayer()
    {
        Assert.That(_testPlayer.AreYou("me"), Is.True);
    }
    [Test]
    public void LocatePlayer()
    {
        Assert.That(_testPlayer.Locate("me"), Is.EqualTo(_testPlayer));
    }
    [Test]
    public void LocateItem()
    {
        Assert.That(_testPlayer.Locate("inventory"), Is.EqualTo(_testPlayer));
    }
    [Test]
    public void LocateNothing()
    {
        Assert.That(_testPlayer.Locate("Nothing"), Is.Null);
    }
    [Test]
    public void PlayerFullDescription()
    {
        Assert.That(_testPlayer.FullDescription, Is.EqualTo($"You are James - an explorer\n" + "You are carrying: " + "a silver hat, a torch"));
    }

    [Test]
    public void LocateItemInLocation()
    {
        //Assert.That(_testPlayer.Locate(_testPlayer.Location), Is.EqualTo("New York"));
    }
}