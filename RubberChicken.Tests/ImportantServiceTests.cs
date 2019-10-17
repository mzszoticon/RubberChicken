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
                Mock.Of<ILogging>(MockBehavior.Strict)
                );

            service.SetInitial("SessionId", "SomeData");

            mocks.VerifyAll();
        }
    }
}
