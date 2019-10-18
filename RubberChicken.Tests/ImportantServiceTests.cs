using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using Wdh.RubberChicken.BL;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.DAL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace RubberChicken.Tests
{
    [TestClass]
    public class ImportantServiceTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var autoMocker = new AutoMocker(MockBehavior.Strict);
            var service = autoMocker.CreateInstance<ImportantService>();
        }

        [TestMethod]
        public void CanSetInitialValue()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var mapper = mocks.Create<ISessionMapper>();
            var persistor = mocks.Create<IPersister>();

            mapper.Setup(s => s.StartOrGetSession(It.IsAny<string>())).Returns<string>(s => $"{s}_dalSession");
            persistor.Setup(s => s.SetData(It.IsAny<string>(), It.IsAny<string>()));

            var service = new ImportantService(
                mapper.Object,
                mocks.Create<IAccessor>().Object,
                persistor.Object,
                Mock.Of<ILogging>()
                );

            service.SetInitial("SessionId", "SomeData");

            mocks.VerifyAll();
        }

        [TestMethod]
        public void CanGetValue()
        {
            var autoMocker = new AutoMocker(MockBehavior.Strict);
            autoMocker.Use<ISessionMapper>(s => s.StartOrGetSession(It.IsAny<string>()) == "SomeSessionId");
            autoMocker.Use<IAccessor>(s => s.GetData(It.IsAny<string>()) == "SomeData");

            var service = autoMocker.CreateInstance<ImportantService>();

            Assert.IsNotNull(service.GetValue("SessionId"));
        }

        [TestMethod]
        public void CanDuplicate()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var mapper = mocks.Create<ISessionMapper>();
            var persistor = mocks.Create<IPersister>();
            var accessor = mocks.Create<IAccessor>();

            mapper.Setup(s => s.StartOrGetSession(It.IsAny<string>())).Returns<string>(s => $"{s}_dalSession");
            persistor.Setup(s => s.SetData(It.IsAny<string>(), It.IsAny<string>()));
            accessor.Setup(s => s.GetData(It.IsAny<string>())).Returns("aaaaaaaa").Verifiable();

            var service = new ImportantService(
                                mapper.Object,
                accessor.Object,
                persistor.Object,
                Mock.Of<ILogging>());

            // Act
            service.Duplicate("SessionId");

            // Assert
            mocks.Verify();
        }

        [TestMethod]
        public void CanTruncate()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var mapper = mocks.Create<ISessionMapper>();
            var persistor = mocks.Create<IPersister>();
            var accessor = mocks.Create<IAccessor>();

            mapper.Setup(s => s.StartOrGetSession(It.IsAny<string>())).Returns<string>(s => $"{s}_dalSession");
            persistor.Setup(s => s.SetData(It.IsAny<string>(), It.IsAny<string>()));
            accessor.Setup(s => s.GetData(It.IsAny<string>())).Returns("aaaaaaaa").Verifiable();

            var service = new ImportantService(
                                mapper.Object,
                accessor.Object,
                persistor.Object,
                Mock.Of<ILogging>());

            // Act
            service.Truncate("SessionId", 2);

            // Assert
            mocks.Verify();
        }
    }
}
