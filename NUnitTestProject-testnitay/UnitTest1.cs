using NUnit.Framework;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace NUnitTestProject_testnitay
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRegister()
        {
            // arrange
            Service s = new Service();
            s.Register("nitayv@gmail.com","Aa123465","nit");
            //act
            s.Login("nitayv@gmail.com", "Aa123465");
            //assert
            Assert.AreEqual(true, s.Login("nitayv@gmail.com", "Aa123465"), "filed to login register email");
        }
    }
}