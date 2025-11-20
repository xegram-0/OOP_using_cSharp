using SwinAdventure;

namespace SwinAdventureTests;

[TestFixture]
public class LookCommandTests
{
    private Item _testItem;
    private Player _testPlayer;
    private Bag _testMoneyBag;
    private LookCommand _testLookCommand;

    [SetUp]
    public void Setup()
    {
        _testItem = new Item(["gem", "Ruby"], "A ruby", "A bright Pink ruby");
        _testLookCommand = new LookCommand([""]);
        _testPlayer = new Player("Harry Potter", "A student");
        _testMoneyBag = new Bag(["bag", "money"], "Money bag", "A bag contains valuables");
        
        _testPlayer.Inventory.Put(_testMoneyBag);
        _testPlayer.Inventory.Put(_testItem);
    }

    [Test]
    public void LookAtPlayer()
    {
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look","at","inventory"]), Is.EqualTo(_testPlayer.FullDescription));
    }
    [Test]
    public void LookAtItem()
    {
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem"]), Is.EqualTo(_testItem.Description));
    }
    [Test]
    public void LookAtNothing()
    {
        _testPlayer.Inventory.Take("gem");
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem"]), Is.EqualTo("I cannot find the gem"));
    }
    [Test]
    public void LookAtItemInPlayer()
    {
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem","in","inventory"]), Is.EqualTo(_testItem.Description));

    }
    [Test]
    public void LookAtItemInBag()
    {
        _testMoneyBag.Inventory().Put(_testItem);
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem","in","bag"]), Is.EqualTo(_testItem.Description));
    }
    [Test]
    public void LookAtItemInNoBag()
    {
        _testPlayer.Inventory.Take("bag");
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem","in","bag"]), Is.EqualTo("I cannot find the bag"));

    }
    [Test]
    public void LookAtNothingInBag()
    {
        
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "gem","in","bag"]), Is.EqualTo("I cannot find the gem"));
    }
    [Test]
    public void InvalidLook()
    {
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "around"]), Is.EqualTo("I don't know how to look like that"));
        Assert.That(_testLookCommand.Execute(_testPlayer, ["hello", "student", "ID"]), Is.EqualTo("Error in look input"));
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "in", "bag"]), Is.EqualTo("What do you want to look at?"));
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "bag","at","inventory"]), Is.EqualTo("What do you want to look in?"));
        Assert.That(_testLookCommand.Execute(_testPlayer, ["look", "at", "sword","in","bag"]), Is.EqualTo("I cannot find the sword"));
    }
}