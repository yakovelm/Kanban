using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace TestsY
{
    [TestClass]
    public class UnitTest1
    {
        [Setup]
        public void ss()
        {

        }
        [TestMethod]
        public void TestMethod1()
        {
            Service e = new Service();
            Response a= e.Register("asd", "asda12", "asd");
            Assert.AreEqual(new Response(), a, "register");
        }
    }
}
