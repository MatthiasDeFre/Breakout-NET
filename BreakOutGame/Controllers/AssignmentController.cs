using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Filters;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BreakOutGame.Models.ViewModels;

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
        public IActionResult Opgave(int groupid, int sessionid)
        {
            BoBGroup bobgroup=_bobsessionRepository.GetSpecificGroupFromSession(groupid, sessionid);
            SessionPath path = bobgroup.Path;
            IEnumerable<Assignment> assignments = path.Assignments;
            Assignment assignment= assignments.First();
            return View(assignment);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToegangVolgendeOpdracht()
        {
            TempData["FouteCode"] = false;
            return View(new AssignmentCodeViewModel());
        }
        [HttpPost]
        public IActionResult ValideerCodeOpdracht(AssignmentCodeViewModel avm)
        {
            //TODO -> temporary false
            TempData["FouteCode"] = true;
            return View("ToegangVolgendeOpdracht", avm);
        }


    }
}