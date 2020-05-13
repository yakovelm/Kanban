using NUnit.Framework;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace TestsUnit
{
    public class TestsUnit
    {
        public Service e;
        [SetUp]
        public void Setup()
        {
            e = new Service();

        }
        [TearDown]
        public void TearDown()
        {
            e.DeleteData();
        }

        [Test]
        public void TestMethod1()
        {
            Response a = e.Register("asdasd", "asdasd", "asd");
            Assert.IsFalse(a.ErrorOccured, "Re");
        }
        [Test]
        public void TestMethod2()
        {

            e.LoadData();
            Response a = e.Register("asdasd@", "asd123AASD", "asd");
            Assert.AreEqual("", a.ErrorMessage, "asdasd");
        }

    }
}