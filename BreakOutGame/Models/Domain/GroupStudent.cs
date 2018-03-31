using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class GroupStudent
    {
        public decimal BoBGroup_ID { get; set; }
        public BoBGroup Group { get; set; }
        public decimal students_ID { get; set; }
        public Student Student { get; set; }
    }
}
