using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreakOutGame.Controllers
{
    public class BoBGroupController : Controller
    {
        private readonly IBoBGroupRepository _boBGroupRepository;
        private readonly IBoBSessionRepository _boBSessionRepository;
        public BoBGroupController(IBoBGroupRepository boBGroupRepository, IBoBSessionRepository boBSessionRepository)
        {
            _boBGroupRepository = boBGroupRepository;
            _boBSessionRepository = boBSessionRepository;
        }
        public IActionResult Index(int? id)
        {
            //   IEnumerable<BoBGroup> groups = _boBGroupRepository.GetAll();
            if (id == null)
            {
                var serSessionId = HttpContext.Session.GetString("SessionId");
                if (serSessionId == null)
                {
                    //Person tried to bruteforce onto page
                    TempData["bruteforce"] = "Gelieve de startpagina te gebruiken om mee te doen aan een sessie";
                    return RedirectToAction("Index", "Home");
                }
                id = Int32.Parse(serSessionId);
            }
            else
            {
                HttpContext.Session.SetString("SessionId", id.Value.ToString());
            }
            //Retrieve session and check if session is activated
            BoBSession session = _boBSessionRepository.GetById(id.Value);
            //if(check session active) => redirect else nothing
            //IEnumerable <BoBGroup> groups = _boBSessionRepository.GetGroupsFromSession(sessionId).OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            IEnumerable<BoBGroup> groups = session.Groups.OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            return View(groups);
        }

        
        public IActionResult WaitScreen(int id)
        {
            //   BoBGroup group = _boBGroupRepository.GetById(id);
            if(!Int32.TryParse(HttpContext.Session.GetString("SessionId"), out var sessionId))
            {
                TempData["bruteforce"] = "Nice try";
                return RedirectToAction("Index", "Home");
            }

            //Make sure the chosen groupId is from the current session
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, id);

            //Chosen group is not from this session
            if (group == null)
            {
                TempData["bruteforce"] = "Nice try";
                return RedirectToAction("Index", "Home");
            }
              
            //Chosen group has already been chosen
            if (group.Status != GroupStatus.NotSelected)
            {
                TempData["groupchosen"] = "Deze groep is toch al gekozen :(";
                return RedirectToAction("Index");
            }

            //Change group status to selected
            group.Status = GroupStatus.Selected;

            //Save Changes to database
            _boBSessionRepository.SaveChanges();
            return View(group);
        }
    }
}