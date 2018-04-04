using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class BoBGroup
    {
        public decimal Id { get; set; }
        public String GroupName { get; set; }
        public IEnumerable<GroupStudent> Students { get; set; }
        public GroupStatus Status { get; set; }
    }
}
