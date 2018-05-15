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
            int percentage = Convert.ToInt32(_bobSessionRepository.GetCompletionPercentage(sessionid, groupid) * 100);
            ViewData["percentage"] = percentage;
            if (assignment.Status == AssignmentStatus.WaitingForCode)
            {
                return RedirectToAction("Action", assignment.ReferenceNr);
            }
            return View(assignment);
        }

        [SessionFilter]
        [GroupFilter]
        [HttpPost]
        public IActionResult ValidateAnswer(int sessionId, int groupId, String answer)
        {
            BoBSession session = _bobSessionRepository.GetById(sessionId);
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            Assignment assignment = group.NextAssignment;
            bool correct = session.ValidateAnswer(group, assignment, answer);
            _bobSessionRepository.SaveChanges();
            if (!correct && group.Status == GroupStatus.Blocked)
            {
                TempData["blocked"] = true;
                //return RedirectToAction("Opgave");
            }
            //return RedirectToAction("Action", assignment.ReferenceNumber);
            //SERIALIZE ACCESSCODE
            if(session.AreActionsEnabled)
                return RedirectToAction("Action", assignment.ReferenceNr);
            return RedirectToAction("Index");
        }

        [SessionFilter]
        public IActionResult Action(int sessionId, int referenceNumber)
        {
            BoBAction action = _bobSessionRepository.GetAction(sessionId, referenceNumber);
            return View(action);
        }

        [HttpPost]
        [SessionFilter]
        [GroupFilter]
        public IActionResult ValidateAccessCode(int sessionId, int groupId, int accessCode)
        {
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            Assignment assignment = group.NextAssignment;
            if (!assignment.ValidateCode(accessCode))
            {
                TempData["wrongaccess"] = true;
                return RedirectToAction("Action");
            }
            _bobSessionRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        [SessionFilter]
        [GroupFilter]
        public IActionResult BlockCurrentGroup(int groupId, int sessionId)
        {
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            group.Block();
            _bobSessionRepository.SaveChanges();
            return RedirectToAction("Index", "Assignment");
        }


    }
}