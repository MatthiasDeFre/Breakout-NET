using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using BreakOutGameTest.Data;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Controllers;
using Microsoft.AspNetCore.Mvc;

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
            this._controller = new SessionController(_sessionRepository.Object);
        }

        #region tests

        [Fact]
        public void SessionCode_Valid_RedirectsTo_BoBGroup_Index()
        {
            //trek een id binnen, get sessie met id, check of deze sessie actief is
            _sessionRepository.Setup(s => s.GetById(1)).Returns(_dummyContext.ValidSession);
            var result = _controller.ValidateSessionCode(1) as RedirectToActionResult;

            Assert.Equal("BoBGroup", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
            //kijk of route parameter opnieuw is meegegeven tijdens de RedirectToAction
            int? number = result.RouteValues["id"] as int?;
            Assert.True(number.HasValue);
            Assert.True(number.Value == 1);
        }

        [Fact]
        public void SessionCode_ScheduledSession_RedirectBackTo_SessionCodeScreen()
        {
            _sessionRepository.Setup(s => s.GetById(2)).Returns(_dummyContext.InvalidSession1);
            var result = _controller.ValidateSessionCode(2) as RedirectToActionResult;

            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
            //temp data checken
        }

        [Fact]
        public void SessionCode_CancelledSession_RedirectBackTo_SessionCodeScreen()
        {
            _sessionRepository.Setup(s => s.GetById(3)).Returns(_dummyContext.InvalidSession2);
            var result = _controller.ValidateSessionCode(3) as RedirectToActionResult;

            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Index", result?.ActionName);
            //temp data checken
        }

        #endregion
    }
}
