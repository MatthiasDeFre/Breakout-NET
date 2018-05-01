using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class PathAssignment
    {
        private int _huidigeOpdracht;
        public int SessionPathId { get; set; }

        public int HuidigeOpdracht { get { return _huidigeOpdracht;} }

        public int AssignmentId { get; set; }
    }
}
