using Xunit;
using Moq;
using BreakOutGameTest.Data;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BreakOutGameTest.Controllers
{
    public class SessionTest
    {
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IBoBSessionRepository> _sessionRepository;
        private readonly SessionController _controller;

        public SessionTest()
        {
            this._dummyContext = new DummyApplicationDbContext();
            this._sessionRepository = new Mock<IBoBSessionRepository>();
            this._controller = new SessionController(_sessionRepository.Object) ;
        }

        #region tests

        [Fact]
        public void SessionCode_Valid_RedirectsTo_BoBGroup_Index()
        {
            //trek een id binnen, get sessie met id, check of deze sessie actief is
            _sessionRepository.Setup(s => s.GetById(1)).Returns(_dummyContext.ValidSession);
            var result = _controller.ActivateSession(1) as RedirectToActionResult;

            Assert.Equal("BoBGroup", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
        }


        [Fact]
        public void SessionCode_ClosedSession_RedirectBackTo_SessionCodeScreen()
        {
            _sessionRepository.Setup(s => s.GetById(2)).Returns(_dummyContext.ClosedSession);
            var result = _controller.ActivateSession(2) as RedirectToActionResult;

            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void SessionCode_ActiveSession_RedirectBackTo_SessionCodeScreen()
        {
            _sessionRepository.Setup(s => s.GetById(3)).Returns(_dummyContext.ActiveSession);
            var result = _controller.ActivateSession(3) as RedirectToActionResult;

            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void SessionCode_StartedSession_RedirectBackTo_SessionCodeScreen()
        {
            _sessionRepository.Setup(s => s.GetById(4)).Returns(_dummyContext.StartedSession);
            var result = _controller.ActivateSession(4) as RedirectToActionResult;

            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
        }

        #endregion
    }
}




