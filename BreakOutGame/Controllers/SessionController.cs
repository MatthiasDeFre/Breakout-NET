using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Filters;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreakOutGame.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SessionController : Controller
    {
        private readonly IBoBSessionRepository _boBSessionRepository;

        /// <summary>
        /// Get main screen to input the sessioncode
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("SessionCodeScreen");
        }


        public SessionController(IBoBSessionRepository boBSessionRepository)
        {
            this._boBSessionRepository = boBSessionRepository;
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [AllowAnonymous]
        public int getTotalAssignmentsForSession(int sessionId)
        {
            //get alle SESSIONPATH_ASSIGNMENTS voor een session path id
            return 0;
        }

        /// <summary>
        /// Check sessioncode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Control(int id)
        {
            BoBSession session = _boBSessionRepository.GetById(id);
            if (session == null)
            {
                TempData["sessionCode"] = "Deze sessiecode bestaat niet";
                return RedirectToAction("Index");
            }
            if (session.SessionStatus != SessionStatus.Activated)
            {
                TempData["sessionCode"] = "Deze sessie is nog niet gestart of is al bezig";
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetInt32("SessionId", id);
            //Keert terug naar het scherm van controller 'BobGroup' naar de html 'Index'
            return RedirectToAction("Index", "BoBGroup");
        }

        /// <summary>
        /// Activate session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ActivateSession(int sessionId)
        {
            HttpContext.Session.SetInt32("SessionId", sessionId);
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            try
            {
                session.Activate();
            }
            catch (InvalidOperationException ex)
            {
                
                return RedirectToAction("Index", "Session");
            }
            _boBSessionRepository.SaveChanges();
            return RedirectToAction(nameof(SessionDetail));
        }

        /// <summary>
        /// Get the list of all the sessions
        /// </summary>
        /// <returns></returns>
        public IActionResult ListSessions()
        {
            IEnumerable<BoBSession> lijst = _boBSessionRepository.GetAll();
            return View("ListSessions",lijst);
        }

      /// <summary>
      /// Start the session
      /// </summary>
      /// <param name="sessionId"></param>
      /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult StartSession(int sessionId)
        {
            _boBSessionRepository.GetById(sessionId).Start();
            _boBSessionRepository.SaveChanges();
            return RedirectToAction(nameof(SessionDetail));
        }

        /// <summary>
        /// Set the sessionId, method needed to get to the detail screen from the list of sessions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetSessionId(int sessionId)
        {
            HttpContext.Session.SetInt32("SessionId", sessionId);
            return RedirectToAction("SessionDetail");
        }

        /// <summary>
        /// Get the details of a session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        
        public IActionResult SessionDetail(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetByIdDetail(sessionId);
            OrderGroupPaths(session.Groups);
            return View("SessionDetail", session);
        }

        /// <summary>
        /// Enable actions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult EnableActions(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.AreActionsEnabled = true;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        /// <summary>
        /// Disable actions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult DisableActions(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.AreActionsEnabled = false;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        /// <summary>
        /// Enable freejoin, students can freely add them to a group
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult EnableFreeJoin(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.IsFreeJoinEnabled = true;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        /// <summary>
        /// Disable freejoin
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult DisableFreeJoin(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.IsFreeJoinEnabled = false;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        /// <summary>
        /// Get all the info from a session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult SessionInfo(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetByIdDetail(sessionId);
           OrderGroupPaths(session.Groups);
            return Json(session);
        }

        /// <summary>
        /// Order group paths
        /// </summary>
        /// <param name="groups"></param>
        private void OrderGroupPaths(IEnumerable<BoBGroup> groups)
        {
            foreach (var boBGroup in groups)
            {
                boBGroup.Path.Assignments = boBGroup.Path.Assignments.OrderBy(g => g.ReferenceNr);
            }
        }

    }
}