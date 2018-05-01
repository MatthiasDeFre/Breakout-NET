using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Filters;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreakOutGame.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IBoBSessionRepository _bobsessionRepository;
        public AssignmentController(IBoBSessionRepository bobsessionRepository)
        {
            _bobsessionRepository = bobsessionRepository;
        }

        [GroupFilter]
        [SessionFilter]
        public IActionResult Index(int groupid, int sessionid)
        {
            BoBGroup bobgroup=_bobsessionRepository.GetSpecificGroupFromSession(groupid, sessionid);
            var serSessionId = HttpContext.Session.GetInt32("SessionId");
            Assignment assignment=_bobsessionRepository.GetNextAssignment((int)serSessionId,bobgroup.Id);
            return View(assignment);
        }

    }
}