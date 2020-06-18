using NUnit.Framework;
using IntroSE.Kanban.Backend.BusinessLayer.BoardControl;
using IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using Moq;
using System.Collections.Generic;

namespace UnitTestBoard
{
    public class Tests
    {
        Board b;
        Mock<List<Column>> columns;
        [SetUp]
        public void Setup()
        {
            columns = new Mock<List<Column>>();
        }

        [Test]
        public void checkTest()
        {
            Assert.AreEqual(b.GetEmail() , "asd" ,"check Test fail");
        }
    }
}