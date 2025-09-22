using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class IdentifierObjTests
    {
        private IdentifiableObj _identifierObj;
        [SetUp]
        public void Setup()
        {
            _identifierObj = new IdentifiableObj(new string[] {"1026", "Lego"});
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

        
    }
}

