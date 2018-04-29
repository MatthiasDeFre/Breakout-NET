using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public class StartedState : SessionState
    {
        public StartedState(BoBSession session) : base(session)
        {
        }
        public override void Lock()
        {
            Session.SessionStatus = SessionStatus.Closed;
            Session.SessionState = new LockedState(Session);
        }
    }
}
