using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class ItemsTests
    {
        private Item _items;

        [SetUp]
        public void Setup()
        {
            _items = new Item(["sword", "swo"], "Sword", "Lord of the word");
        }

        [Test]
        public void TestItemIsIdentifiable()
        {
            Assert.That(_items.AreYou("sword"), Is.True);
        }
        [Test]
        public void TestShortDescription()
        {
            Assert.That(_items.ShortDescription, Is.EqualTo("Sword"));
        }
        [Test]
        public void TestLongDescription()
        {
            Assert.That(_items.FullDescription, Is.EqualTo("Lord of the word"));
        }
        
        [Test]
        public void TestPrivilegeEscalation()
        {
            _items.PrivilegeEscalation("2476");
            Assert.That(_items.FirstId, Is.EqualTo("0007"));
        }
        /*
               [Test]
               public void TestDuplicatedBlocked()
               {
                   _items.AddIdentifier("SWORD");
                   Assert.That(_items.PrintNumIdentifiers(), Is.EqualTo(2));
               }
     
        [Test]
        public void TestRemoveIdentifier()
        {
            _items.RemoveIdentifier("sword");
            Assert.That(_items.AreYou("sword"), Is.False);
        }
        */   
    }
}

