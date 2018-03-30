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
        public IEnumerable<Student> Students { get; set; }
    }
}
