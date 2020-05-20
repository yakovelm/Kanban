using NUnit.Framework;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace UnitTests
{
    public class Tests
    {
        private Service s;
        [SetUp]
        public void Setup()
        {
            s = new Service();
        }
        [TearDown]
        public void teardawn()
        {
            s.DeleteData();
        }

        [Test]
        public void Test1()
        {
            Assert.IsFalse(s.Register("asd@asasdasdasdasdasdasd.asd", "asd123Aas", "asd").ErrorOccured, "cant register asd.");
        }


    }
}