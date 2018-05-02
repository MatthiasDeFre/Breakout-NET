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
        private readonly IBoBSessionRepository _bobSessionRepository;
        public AssignmentController(IBoBSessionRepository bobSessionRepository)
        {
            _bobSessionRepository = bobSessionRepository;
        }

        [GroupFilter]
        [SessionFilter]
        public IActionResult Index(int groupid, int sessionid)
        {
          //  BoBGroup bobgroup=_bobSessionRepository.GetSpecificGroupFromSession(sessionid, groupid);
         
            Assignment assignment=_bobSessionRepository.GetNextAssignment(sessionid,groupid);
            if (assignment == null)
                return RedirectToAction("Index", "Home");
            return View(assignment);
        }

        [SessionFilter]
        [GroupFilter]
        [HttpPost]
        public IActionResult ValidateAnswer(int sessionId, int groupId, String answer)
        {
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            Assignment assignment = group.NextAssignment;
            bool correct = group.ValidateAnswer(assignment, answer);
            _bobSessionRepository.SaveChanges();
            if (!correct && group.Status == GroupStatus.Blocked)
            {
                TempData["blocked"] = true;
                //return RedirectToAction("Opgave");
            }
            //return RedirectToAction("Action", assignment.ReferenceNumber);
            //SERIALIZE ACCESSCODE
            return RedirectToAction("Index");
        }

    }
}