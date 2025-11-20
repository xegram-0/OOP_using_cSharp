namespace SwinAdventureTests;
using SwinAdventure;
[TestFixture]
public class PathTests
{
    private SwinAdventure.Path _path;
    private Player _player;
    private Location _location;
    private Location _location2;
    [SetUp]
    public void SetUp()
    {
        _player = new Player("James", "an explorer");
        _location = new Location("sea", "sea with lots of fish");
        _location2 = new Location("mountain", "mountain with lots of cliffs");
        _path = new Path(["west", "the west"], "west", 
            "path to the sea", _location, _location2);
        _location.AddPath(_path);
        _player.Location = _location;
    }
    [Test]
    public void TestPathIsBlocked()
    {
        Assert.That(_path.IsBlocked, Is.False);
    }

    [Test]
    public void TestPathSource()
    {
        Assert.That(_path.Source, Is.EqualTo(_location));
    }
    [Test]
    public void TestPathDestination()
    {
        Assert.That(_path.Destination, Is.EqualTo(_location2));
    }

    [Test]
    public void TestPathLocate()
    {
        Assert.That(_player.Locate("west"), Is.EqualTo(_path.Destination));
    }

    [Test]
    public void TestPathLocateNothing()
    {
        Assert.That(_player.Locate("nowhere"), Is.Null);
    }

    [Test]
    public void TestPathDescription()
    {
        Assert.That(_path.Description, Is.EqualTo("path to the sea"));
    }

    [Test]
    public void TestPathList()
    {
        Assert.That(_location.PathList, Is.EqualTo("You can reach: west"));
    }

    [Test]
    public void TestPathListEmpty()
    {
        Location location3 = new Location("city", "city with lots of people");
        Assert.That(location3.PathList, Is.EqualTo("No paths lead to other locations"));
    }
}