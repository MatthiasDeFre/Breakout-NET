using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BreakOutGame.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBoBSessionRepository _boBSessionRepository;
        public IActionResult Index()
        {
            return View("SessionCodeScreen");
        }

        public SessionController(IBoBSessionRepository boBSessionRepository)
        {
            this._boBSessionRepository = boBSessionRepository;
        }
        
    }
}