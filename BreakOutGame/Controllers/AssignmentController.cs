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
using Microsoft.AspNetCore.Routing;

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
        public IActionResult Index(int groupId, int sessionId)
        {
          //  BoBGroup bobgroup=_bobSessionRepository.GetSpecificGroupFromSession(sessionid, groupid);
         
            Assignment assignment=_bobSessionRepository.GetNextAssignment(sessionId,groupId);
            if (assignment == null)
            {
                if (groupId == 0 || sessionId == 0)
                {
                    return RedirectToAction("Index", "Session");
                }
                return View("EndScreen");
            }
                
            int percentage = Convert.ToInt32(_bobSessionRepository.GetCompletionPercentage(sessionId, groupId) * 100);
            ViewData["percentage"] = percentage;
            if (assignment.Status == AssignmentStatus.WaitingForCode)
            {
                return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}
                });
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
            bool correct = false;
            Assignment assignment = group.NextAssignment;
            try
            {
                correct = session.ValidateAnswer(group, assignment, answer);
            }
            catch (InvalidOperationException ex)
            {
                TempData["blocked"] = "Roep de leerkracht om te vragen om je te deblokkeren";
                return RedirectToAction("Index");
            }
            
            _bobSessionRepository.SaveChanges();
            if (!correct)
            {
                TempData["wronganswer"] = answer + " was niet het juiste antwoord";
                if (group.Status == GroupStatus.Blocked)
                {
                    TempData["blocked"] = "Je bent geblokkeerd";
                }
                if (session.IsFeedbackEnabled && group.NextAssignment.WrongCount >= 3)
                {
                    TempData["feedback"] = "Bekijk de feedback om dit hoofdstuk terug op te helderen";
                }
                return RedirectToAction("Index");

                //return RedirectToAction("Opgave");
            }
            //return RedirectToAction("Action", assignment.ReferenceNumber);
            //SERIALIZE ACCESSCODE
            if (session.AreActionsEnabled && !session.IsDistant)
                return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}
                });
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
                return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}
                });
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