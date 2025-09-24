using SwinAdventure;

namespace SwinAdventureTests;

[TestFixture]
public class InventoryTests
{
    private Inventory  _inventory;

    [SetUp]
    public void Setup()
        {
            _inventory = new Inventory();
        }

    [Test]
    public void TestAreYou()
        {
            Assert.That(_inventory.HasItem("Gun")),Is.True);
        }
}