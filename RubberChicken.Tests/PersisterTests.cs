using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using Wdh.RubberChicken.DAL;
using Wdh.RubberChicken.DAL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace RubberChicken.Tests
{
    [TestClass]
    public sealed class PersisterTests
    {
        private const string Data = "SomeData";

        [TestMethod]
        public void SmokeTest()
        {
            // Should not throw
            var mocker = new AutoMocker();
            var persister = mocker.CreateInstance<Persister>();

            Assert.IsNotNull(persister);
        }

        [TestMethod]
        public void TestGetData()
        {
            Mock<ISessionProvider> provider = new Mock<ISessionProvider>();
            var session = Mock.Of<ISession>(s => s.GetData() == Data);
            provider.Setup(s => s.GetSession("SessionId")).Returns(session);

            // Should not throw
            var persister = new Persister(
                provider.Object,
                Mock.Of<ILogging>()
                );

            Assert.AreEqual(Data, persister.GetData("SessionId"));
        }

        [TestMethod]
        public void TestSetData()
        {
            // Should not throw
            var persister = new Persister(
                Mock.Of<ISessionProvider>(m => m.GetSession(It.IsAny<string>()) == Mock.Of<ISession>()),
                Mock.Of<ILogging>()
                );

            persister.SetData("SessionId", "aaaaa");
        }
    }
}
