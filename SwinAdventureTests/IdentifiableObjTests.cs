using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class IdentifierObjTests
    {
        private IdentifiableObj _identifierObj;
        private IdentifiableObj _notIdentifiableObj;
        [SetUp]
        public void Setup()
        {
            _identifierObj = new IdentifiableObj(new string[] {"1026", "Lego"});
            _notIdentifiableObj = new IdentifiableObj(new string[] {""});
        }

        [Test]
        public void TestAreYou()
        {
            Assert.That(_identifierObj.AreYou("1026"),Is.True);
        }
    
        [Test]
        public void TestNotAreYou()
        {
            Assert.That(_identifierObj.AreYou("1000"),Is.False);
        }
        
        [Test]
        public void TestCaseSensitive()
        {
            Assert.That(_identifierObj.AreYou("lEgO"),Is.True);
        }

        [Test]
        public void TestFirstId()
        {
            Assert.That(_identifierObj.FirsId,Is.EqualTo("1026"));
        }

        [Test]
        public void TestSecondId()
        {
            Assert.That(_notIdentifiableObj.FirsId,Is.EqualTo(""));
        }

        [Test]
        public void TestAddId()
        {
            _identifierObj.AddIdentifier("Huge");
            Assert.That(_identifierObj.AreYou("Huge"), Is.True);
        }

        [Test]
        public void TestPrivilegeEscalation()
        {
            _identifierObj.PrivilegeEscalation("2476");
            Assert.That(_identifierObj.FirsId, Is.EqualTo("0007"));
        }
    }
}

