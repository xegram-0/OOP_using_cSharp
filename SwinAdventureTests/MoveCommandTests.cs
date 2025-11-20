namespace SwinAdventureTests;
using SwinAdventure;
[TestFixture]
public class MoveCommandTests
{
    private SwinAdventure.Path _path;
    private Player _player;
    private Location _location;
    private Location _location2;
    private MoveCommand _command;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("James", "an explorer");
        _location = new Location("sea", "sea with lots of fish");
        _location2 = new Location("mountain", "mountain with lots of cliffs");
        _path = new SwinAdventure.Path(["west", "the west"], "west", 
            "journey to the west", _location, _location2);
        _location.AddPath(_path);
        _player.Location = _location;
        _command = new MoveCommand(["move"]);
    }

    [Test]
    public void TestMoveToBlockedPath()
    {
        _path.IsBlocked = true;
        Assert.That(_command.Execute(_player, ["move", "west"]), Is.EqualTo("west blocked"));
    }

    [Test]
    public void TestMoveToNowhere()
    {
        Assert.That(_command.Execute(_player, ["go", "nowhere"]), Is.EqualTo("nowhere unreachable"));
    }
    [Test]
    public void TestMoveDestination()
    {
        Assert.That(_command.Execute(_player, ["go", "west"]), Is.EqualTo("west reached"));
    }

    [Test]
    public void InvalidMovement()
    {
        Assert.That(_command.Execute(_player, ["delay"]), Is.EqualTo("Unknown move input"));
        Assert.That(_command.Execute(_player, ["go", "north","then", "east"]), Is.EqualTo("Unknown movement"));
        Assert.That(_command.Execute(_player, ["move", "back", "north"]), Is.EqualTo("To where you want?"));
    }
}