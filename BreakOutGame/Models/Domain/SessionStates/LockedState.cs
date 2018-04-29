using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public class LockedState : SessionState
    {
        public LockedState(BoBSession session) : base(session)
        {
        }

        public override void Unlock()
        {
            Session.SessionStatus = SessionStatus.Started;
            Session.SessionState = new StartedState(Session);
        }

    }
}
