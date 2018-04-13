using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class BoBAction
    {
        public int Id { get; set; }
        public String Action { get; set; }
       
        public IEnumerable<SessionAction> Session { get; set; }
    }
}
