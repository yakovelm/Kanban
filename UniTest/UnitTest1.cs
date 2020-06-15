using NUnit.Framework;
using IntroSE.Kanban.Backend.BusinessLayer.BoardControl;
using IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using Moq;


namespace UniTest
{

    public class Tests
    {
        Board b;
        Mock<Column> a;
        Mock c;
        Mock<Task> Daa;
        [SetUp]
        public void Setup()
        {
            b = new Board();
            a = new Mock<Column>();
            c = new Mock<IColumn>();
            Daa = new Mock<Task>();
        }

        [TestCase(10)]
        [Test]
        public void Test1(int T)
        {
            
        }
    }
}