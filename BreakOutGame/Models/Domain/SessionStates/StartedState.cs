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
        public override bool ValidateAnswer(BoBGroup group, Assignment assignment, string answer)
        {
            return group.ValidateAnswer(assignment, answer, !Session.IsDistant && Session.AreActionsEnabled, !Session.IsFeedbackEnabled);
        }

        public override bool ValidateCode(BoBGroup group, Assignment assignment, int code)
        {
            return group.ValidateCode(assignment, code);
        }
    }
}
