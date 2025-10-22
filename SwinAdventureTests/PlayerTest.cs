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
        Assert.That(_testPlayer.FullDescription, Is.EqualTo($"You are James an explorer\n" + "You are carrying: \n" + "A silver hat silver,a torch light"));
    }
}