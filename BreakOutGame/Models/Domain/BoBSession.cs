using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class BoBSession
    {
        private int _sessionStatus;
        public int Id { get; set; }
        public IEnumerable<BoBGroup> Groups { get; set; }
        public int BoxId { get; set; }
        public IEnumerable<SessionAction> Actions { get; set; }

        public SessionStatus SessionStatus { get; set; }
    }
}
