﻿using System;
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
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("SessionCodeScreen");
        }

        public SessionController(IBoBSessionRepository boBSessionRepository)
        {
            this._boBSessionRepository = boBSessionRepository;
        }

        [SessionFilter]
        [AllowAnonymous]
        public int getTotalAssignmentsForSession(int sessionId)
        {
            //get alle SESSIONPATH_ASSIGNMENTS voor een session path id
            return 0;
        }


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
            HttpContext.Session.SetInt32("SessionId", id);
            //Keert terug naar het scherm van controller 'BobGroup' naar de html 'Index'
            return RedirectToAction("Index", "BoBGroup");
        }

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

        public IActionResult ListSessions()
        {
            IEnumerable<BoBSession> lijst = _boBSessionRepository.GetAll();
            return View("ListSessions",lijst);
        }

      
        [SessionFilter]
        [HttpPost]
        public IActionResult StartSession(int sessionId)
        {
            _boBSessionRepository.GetById(sessionId).Start();
            _boBSessionRepository.SaveChanges();
            return RedirectToAction(nameof(SessionDetail));
        }

        [HttpPost]
        public IActionResult SetSessionId(int sessionId)
        {
            HttpContext.Session.SetInt32("SessionId", sessionId);
            return RedirectToAction("SessionDetail");
        }

        [SessionFilter]
        
        public IActionResult SessionDetail(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetByIdDetail(sessionId);
            OrderGroupPaths(session.Groups);
            return View("SessionDetail", session);
        }

        [SessionFilter]
        [HttpPost]
        public IActionResult EnableActions(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.AreActionsEnabled = true;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        [SessionFilter]
        [HttpPost]
        public IActionResult DisableActions(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.AreActionsEnabled = false;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }
        [SessionFilter]
        [HttpPost]
        public IActionResult EnableFreeJoin(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.IsFreeJoinEnabled = true;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        [SessionFilter]
        [HttpPost]
        public IActionResult DisableFreeJoin(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetById(sessionId);
            session.IsFreeJoinEnabled = false;
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail");
        }

        [SessionFilter]
        public IActionResult SessionInfo(int sessionId)
        {
            BoBSession session = _boBSessionRepository.GetByIdDetail(sessionId);
           OrderGroupPaths(session.Groups);
            return Json(session);
        }

        private void OrderGroupPaths(IEnumerable<BoBGroup> groups)
        {
            foreach (var boBGroup in groups)
            {
                boBGroup.Path.Assignments = boBGroup.Path.Assignments.OrderBy(g => g.ReferenceNr);
            }
        }

    }
}