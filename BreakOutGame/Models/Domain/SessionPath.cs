using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class SessionPath
    {
        public int Id { get; set; }

        public IEnumerable<Assignment> Assignments { get; set; }
     

    }
}
