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

        /// <summary>
        /// Method to get the next assignment of the given group. 
        /// The next assignment is the assignment which has the status 'NotCompleted' or 'WaitingForCode'
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [GroupFilter]
        [SessionFilter]
        public IActionResult Index(int groupId, int sessionId)
        {

            //Get assignment from repo
            Assignment assignment = _bobSessionRepository.GetNextAssignment(sessionId, groupId);

            //If their is no assignment found it can mean 2 things
            if (assignment == null)
            {
                //You cheated by deleting the cookie
                if (groupId == 0 || sessionId == 0)
                {
                    return RedirectToAction("Index", "Session");
                }
                //You completed all the assignments
                return View("EndScreen");
            }

            //Retrieve current progress from repo
            SessionProgress progress = _bobSessionRepository.GetCompletionPercentage(sessionId, groupId);
            double percentage = progress.getSessionProgress();
            ViewData["qamount"] = progress.Current + "/" + progress.Total;
            ViewData["percentage"] = (int)percentage;

            //If the assignment is answered but not yet fully completed. Redirect to action Action
            if (assignment.Status == AssignmentStatus.WaitingForCode)
            {
                return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}
                });
            }

            //Show the assignment
            return View(assignment);
        }

        /// <summary>
        /// Validate the answer
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="groupId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [SessionFilter]
        [GroupFilter]
        [HttpPost]
        public IActionResult ValidateAnswer(int sessionId, int groupId, String answer)
        {
            //Get session and group from repo
            BoBSession session = _bobSessionRepository.GetById(sessionId);
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            bool correct = false;

            //Get the next assignment, cannot use assignment method from repo because we need the groupstate to check
            Assignment assignment = group.NextAssignment;
            try
            {
                //Validate answer
                correct = session.ValidateAnswer(group, assignment, answer);
            }
            catch (InvalidOperationException ex)
            {
                //Groupstate not valid
                TempData["blocked"] = "Roep de leerkracht om te vragen om je te deblokkeren";
                return RedirectToAction("Index");
            }

            _bobSessionRepository.SaveChanges();

            //Is the answer incorrect?
            if (!correct)
            {
                //Show last answer
                TempData["wronganswer"] = answer + " was niet het juiste antwoord";

                //Is group blocked?
                if (group.Status == GroupStatus.Blocked)
                {
                    //yep
                    TempData["blocked"] = "Je bent geblokkeerd";
                }

                //Is feedback enabled and answercount higher than 3?
                if (session.IsFeedbackEnabled && group.NextAssignment.WrongCount >= 3)
                {
                    TempData["feedback"] = "Bekijk de feedback om dit hoofdstuk terug op te helderen";
                }

                //Try again
                return RedirectToAction("Index");


            }

            //Answer is correct, check if we need to show the next action 
            if (session.AreActionsEnabled && !session.IsDistant)
                return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}

                });

            //Answer is correct, actions not enabled, show next assignment
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Show action based on referencenumber
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult Action(int sessionId, int referenceNumber)
        {
            BoBAction action = _bobSessionRepository.GetAction(sessionId, referenceNumber);
            return View(action);
        }

        /// <summary>
        /// Validate the given accesscode with the accesscode of the next assignment with status 'WaitingForCode'
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="groupId"></param>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        [HttpPost]
        [SessionFilter]
        [GroupFilter]
        public IActionResult ValidateAccessCode(int sessionId, int groupId, int accessCode)
        {
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);

            //Get next assignment
            Assignment assignment = group.NextAssignment;

            //Code is wrong
            try
            {
                if (!assignment.ValidateCode(accessCode))
                {
                    //Try again
                    TempData["wrongaccess"] = true;
                    return RedirectToAction("Action", new RouteValueDictionary
                {
                    {"referenceNumber", assignment.ReferenceNr}
                });
                }
            }
            catch (InvalidOperationException ex)
            {
                //Groupstate not valid
                TempData["blocked"] = "Roep de leerkracht om te vragen om je te deblokkeren";
                return RedirectToAction("Index");
            }

            _bobSessionRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Block the current group, used in exercise timer
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [GroupFilter]
        public IActionResult BlockCurrentGroup(int groupId, int sessionId)
        {
            //Get group
            BoBGroup group = _bobSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);

            //Block group, no need for try catch, method only called when group would be in 1 of 2 states, blocked and Locked both states implement Block
            group.Block();
            _bobSessionRepository.SaveChanges();

            //Go back to the assignment
            return RedirectToAction("Index", "Assignment");
        }


    }
}