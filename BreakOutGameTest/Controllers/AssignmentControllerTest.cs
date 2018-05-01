using System;
using System.Collections.Generic;
using System.Text;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGameTest.Data;
using Moq;

namespace BreakOutGameTest.Controllers
{
    public class AssignmentControllerTest
    {
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<IBoBSessionRepository> _sessionRepository;

        public AssignmentControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            
        }
    }
}
