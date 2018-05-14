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
            bool allLocked =Session.Groups.All(g => g.Status == GroupStatus.Locked);
            if (allLocked)
            {
                Session.SessionStatus = SessionStatus.Activated;
                Session.SessionState = new ActivatedState(Session);
            }
            else
            {
                throw new InvalidOperationException("Alle groepen moet vergrendeld zijn");
            }
         
        }
    }
}

