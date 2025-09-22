using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class ItemsTests
    {
        private Items _items;

        [SetUp]
        public void Setup()
        {
            _items = new Items(new string[] { "sword", "swo", "SWORD" }, "Lord of the word", "Sword");
        }

        [Test]
        public void TestItemIsIdentifiable()
        {
            Assert.That(_items.AreYou("sword"), Is.True);
        }

        [Test]
        public void TestLongDescription()
        {
            Assert.That(_items.LongDescription, Is.EqualTo("Sword"));
        }

        [Test]
        public bool TestPrivilegeEscalation()
        {
            Assert.That(_items.PrivilegeEscalation("2476"), Is.EqualTo("0007"));
        }
    }
}

