using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Util;
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
        public IActionResult Index()
        {
            //   IEnumerable<BoBGroup> groups = _boBGroupRepository.GetAll();


            IEnumerable<BoBGroup> groups = _boBSessionRepository.GetGroupsFromSession(1).OrderBy(g => g.Status, new GroupStatusComparer()).ThenBy(g => g.GroupName, new GroupNameComparer());
            return View(groups);
        }

        
        public IActionResult WaitScreen(decimal id)
        {
            BoBGroup group = _boBGroupRepository.GetById(Convert.ToInt64(id));
            return View(group);
        }
    }
}