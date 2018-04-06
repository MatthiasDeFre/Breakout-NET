using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class GroupStudent
    {
        public int BoBGroup_ID { get; set; }
        public BoBGroup Group { get; set; }
        public int students_ID { get; set; }
        public Student Student { get; set; }
    }
}
