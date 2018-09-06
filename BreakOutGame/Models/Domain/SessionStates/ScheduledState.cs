using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public class ScheduledState : SessionState
    {
        public ScheduledState(BoBSession session) : base(session)
        {
        }

        public override void Activate()
        {
           
                Session.SessionStatus = SessionStatus.Activated;
                Session.SessionState = new ActivatedState(Session);
         
        }
    }
}

