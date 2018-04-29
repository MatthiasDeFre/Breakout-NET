using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public static class SessionStateFactory
    {
        public static SessionState CreateState(BoBSession session, SessionStatus status)
        {
            SessionState state = null;
            switch (status)
            {
                case SessionStatus.Scheduled:
                    state = new ScheduledState(session);
                    break;
                case SessionStatus.Activated:
                    state = new ActivatedState(session);
                    break; ;
                case SessionStatus.Started:
                    state = new StartedState(session);
                    break;
                case SessionStatus.Closed:
                    state = new LockedState(session);
                    break;
            }
            return state;
        }

    }
}
