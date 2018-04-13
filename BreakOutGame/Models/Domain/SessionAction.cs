using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class SessionAction
    {
        public int BoxId { get; set; }
        public BoBSession Session { get; set; }
        public int ActionId { get; set; }
        public BoBAction Action { get; set; }
        public int OrderNumber { get; set; }
    }
}
