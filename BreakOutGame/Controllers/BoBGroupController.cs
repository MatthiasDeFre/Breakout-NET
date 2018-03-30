using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BreakOutGame.Controllers
{
    public class BoBGroupController : Controller
    {
        private readonly IBoBGroupRepository _boBGroupRepository;

        public BoBGroupController(IBoBGroupRepository boBGroupRepository)
        {
            _boBGroupRepository = boBGroupRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<BoBGroup> groups = _boBGroupRepository.GetAll();
            return View(groups);
        }

        
        public IActionResult WaitScreen(decimal id)
        {
            BoBGroup group = _boBGroupRepository.GetById(Convert.ToInt64(id));
            return View(group);
        }
    }
}