using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Filters;
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
        /// Method to retrieve the overview of groups
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult Index(int sessionId)
        {
           
            //Already chosen a group => deselect current group
            try
            {
                CheckForCurrentGroup(sessionId);
            }
            catch(InvalidOperationException ex)
            {
                TempData["runaway"] = "Niet proberen weglopen!";
                return RedirectToAction("WaitScreen");
            }
          
            BoBSession session = _boBSessionRepository.GetById(sessionId);
    
            IEnumerable<BoBGroup> groups = session.Groups.OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            return View(groups);
        }

        /// <summary>
        /// Method to choose a group based on the group id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [HttpPost]
        [SessionFilter]
        public IActionResult WaitScreen(int id, int sessionId)
        {
            
            //Already chose a group
            CheckForCurrentGroup(sessionId);

            BoBSession session = _boBSessionRepository.GetById(sessionId);
            ViewData["freejoin"] = session.IsFreeJoinEnabled;

            //Make sure the chosen groupId is from the current session
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, id);

            //Chosen group is not from this session
            if (group == null)
            {
                TempData["bruteforce"] = "Groep is niet van de geselecteerde sessie";
                return RedirectToAction("Index", "Home");
            }

            //Chosen group has already been chosen
            try
            {
                session.SelectGroup(group);
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

        /// <summary>
        /// Waitingroom, students will wait in this view untill session is started
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
       [SessionFilter]
        [ResponseCache(VaryByHeader = "*", NoStore = true)]
        public IActionResult WaitScreen(int sessionId)
       {

           BoBSession boBSession = _boBSessionRepository.GetById(sessionId);

            //Are students able to join?
            ViewData["freejoin"] = boBSession.IsFreeJoinEnabled;

            //Could use filter
            int? groupId = HttpContext.Session.GetInt32("groupId");
            if (!groupId.HasValue)
            {
                TempData["groupchosen"]= "Kies een groep aub";
                return RedirectToAction("Index");
            }
              

            BoBGroup group = _boBGroupRepository.GetById(groupId.Value);
            return View(group);
        }


        
        private int? CheckForCurrentGroup(int sessionId)
        {
            //Get current groupId
            int? groupId = HttpContext.Session.GetInt32("groupId");
            BoBSession session = _boBSessionRepository.GetById(sessionId);

            //Deselect current group, there is a current group
            if (groupId.HasValue)
            {
                int groupIdVal = groupId.Value;
                BoBGroup group = _boBGroupRepository.GetById(groupIdVal);
                session.DeselectGroup(group);
                _boBGroupRepository.SaveChanges();
                TempData["groupchosen"] = "De vorig geselecteerde groep is nu niet meer geselecteerd!";
                HttpContext.Session.Remove("groupId");
            }
            return groupId;
        }

        /// <summary>
        /// Lock a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult LockGroup(int groupId, int sessionId)
        {
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            group.Lock(true);
            _boBSessionRepository.SaveChanges();

            return RedirectToAction("SessionDetail", "Session");

        }

        /// <summary>
        /// Block a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult BlockGroup(int groupId, int sessionId)
        {
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            group.Block();
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail","Session");
        }

        /// <summary>
        /// Deblock a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult DeblockGroup(int groupId, int sessionId)
        {
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            if (group.Status == GroupStatus.Blocked)
            {
                group.Deblock();
                _boBSessionRepository.SaveChanges();
            }
            return RedirectToAction("SessionDetail", "Session");
        }

        /// <summary>
        /// Block all groups
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult BlockAllGroups(int sessionId)
        {
            IEnumerable<BoBGroup> groups = _boBSessionRepository.GetGroupsFromSession(sessionId).Where(g => g.Status == GroupStatus.Locked);
            foreach (BoBGroup group in groups)
            {
                    group.Block();
            }
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail", "Session");

        }

        /// <summary>
        /// Deblock all groups
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        [HttpPost]
        public IActionResult DeblockAllGroups(int sessionId)
        {
            IEnumerable<BoBGroup> groups = _boBSessionRepository.GetGroupsFromSession(sessionId).Where(g => g.Status == GroupStatus.Blocked);
            foreach (BoBGroup group in groups)
            {       
                    group.Deblock();
            }
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("SessionDetail", "Session");
        }

       
        //???????????
        /// <summary>
        /// Check if session has started
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [SessionFilter]
        public IActionResult GetSessionStatus(int sessionId)
        {
            BoBSession boBSession = _boBSessionRepository.GetById(sessionId);
            bool active = boBSession.SessionStatus == SessionStatus.Started;
            return Json(active);
        }

        /// <summary>
        /// Add student to group
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="groupId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [SessionFilter]
        [GroupFilter]
        [HttpPost]
        public IActionResult AddToGroup(int sessionId, int groupId, String studentId)
        {
            //Get student from class from session
            Student student= _boBSessionRepository.GetStudentFromSession(sessionId, studentId);
            if (student == null)
            {
                TempData["nostudent"] = "Student bestaat niet :'(";
                return RedirectToAction("WaitScreen"); 
            }
            //If null redirect and tempdat
            //Add student from "" to group
            if (_boBSessionRepository.IsStudentInGroup(sessionId, studentId))
            {
                TempData["nostudent"] = "Student zit al in een groep:'(";
                return RedirectToAction("WaitScreen");
            }
            BoBGroup group = _boBSessionRepository.GetSpecificGroupFromSession(sessionId, groupId);
            group.AddStudent(student);
            //Save changes
            _boBSessionRepository.SaveChanges();
            return RedirectToAction("WaitScreen");
        }
    }
}