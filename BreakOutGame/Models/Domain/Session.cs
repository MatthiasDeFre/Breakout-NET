using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class BoBSession
    {
        public decimal Id { get; set; }
        public IEnumerable<BoBGroup> Groups { get; set; }
    }
}
