using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wdh.RubberChicken.DAL;
using Wdh.RubberChicken.DAL.Interfaces;

namespace RubberChicken.Tests
{
    [TestClass]
    public class SessionManagerTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var test = new SessionManager();
        }

        [TestMethod]
        public void SessionStorage_CanCreateSession()
        {
            var sut = new SessionManager();

            sut.StartSession("SessionId");
        }

        [TestMethod]
        public void SessionStorage_CanCloseSession()
        {
            var sut = new SessionManager();

            sut.StartSession("SessionId");
        }

        [TestMethod]
        public void SessionStorage_CanGetSession()
        {
            var sut = new SessionManager();

            sut.StartSession("SessionId");
            Assert.IsNotNull(((ISessionProvider)sut).GetSession("SessionId"));
        }
    }
}
