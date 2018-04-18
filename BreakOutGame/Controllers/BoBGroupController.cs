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
        /// <summary>
        /// Method to retrieve the overview of groups, parameter should only be used if the teacher gave a direct link to the students
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(int? id)
        {
            //   IEnumerable<BoBGroup> groups = _boBGroupRepository.GetAll();
            //Already chosen a group => deselect current group
            CheckForCurrentGroup();
            id = 1;
            //if (!id.HasValue)
            //{
            //    var serSessionId = HttpContext.Session.GetInt32("SessionId");
            
            //    if (serSessionId == null)
            //    {
            //        //Person tried to bruteforce onto page
            //        TempData["bruteforce"] = "Gelieve de startpagina te gebruiken om mee te doen aan een sessie";
            //        return RedirectToAction("Index", "Home");
            //    }
            //    id = serSessionId;
            //}
            //else
            //{
                HttpContext.Session.SetInt32("SessionId", id.Value);
            //}
            Console.WriteLine(id);
            //Retrieve session and check if session is activated
            BoBSession session = _boBSessionRepository.GetById(id.Value);
            //if(check session active) => redirect else nothing
            //IEnumerable <BoBGroup> groups = _boBSessionRepository.GetGroupsFromSession(id).OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            IEnumerable<BoBGroup> groups = session.Groups.OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            return View(groups);
        }

        public IActionResult WaitScreen()
        {
            int? groupId = HttpContext.Session.GetInt32("groupId");
            if (!groupId.HasValue)
                return RedirectToAction("Index");

            BoBGroup group = _boBGroupRepository.GetById(groupId.Value);
            return View(group);
        }

        [HttpPost]
        public IActionResult WaitScreen(int id)
        {
            
            CheckForCurrentGroup();

            int? sessionId = HttpContext.Session.GetInt32("SessionId");
            //   BoBGroup group = _boBGroupRepository.GetById(id);
            if (!sessionId.HasValue)
            {
                TempData["bruteforce"] = "Zonder sessie";
                return RedirectToAction("Index", "Home");
            }

            //Make sure the chosen groupId is from the current session
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId.Value, id);

            //Chosen group is not from this session
            if (group == null)
            {
                TempData["bruteforce"] = "Groep is niet van de geselecteerde sessie";
                return RedirectToAction("Index", "Home");
            }

            //Chosen group has already been chosen
            try
            {
                group.Select();
            }
            catch (InvalidOperationException ex)
            {
                TempData["groupchosen"] = ex.Message;
                return RedirectToAction("Index", null);
            }

            //Save Changes to database
            _boBSessionRepository.SaveChanges();
            HttpContext.Session.SetInt32("groupId", group.Id);
            return RedirectToAction("WaitScreen");
        }

        private void CheckForCurrentGroup()
        {
            int? groupId = HttpContext.Session.GetInt32("groupId");
            
            //Deselect current group
            if (groupId.HasValue)
            {
                int groupIdVal = groupId.Value;
                BoBGroup group = _boBGroupRepository.GetById(groupIdVal);
                group.Deselect();
                _boBGroupRepository.SaveChanges();
                TempData["groupchosen"] = "De vorig geselecteerde groep is nu niet meer geselecteerd!";
                HttpContext.Session.Remove("groupId");
            }
        }
    }
}