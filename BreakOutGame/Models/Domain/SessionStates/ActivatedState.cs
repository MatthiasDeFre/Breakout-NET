using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public class ActivatedState : SessionState
    {
        public ActivatedState(BoBSession session) : base(session)
        {
        }

        public override void Start()
        {
            Session.SessionStatus = SessionStatus.Started;
            Session.SessionState = new StartedState(Session);
        }

        public override void SelectGroup(BoBGroup group)
        {
            group.Select();
        }

        public override void DeselectGroup(BoBGroup group)
        {
            group.Deselect();
        }

     /*   public override bool ValidateAnswer(BoBGroup group, Assignment assignment, string answer)
        {
            return group.ValidateAnswer(assignment, answer, Session.IsDistant || Session.AreActionsEnabled, !Session.IsFeedbackEnabled);
        }

        public override bool ValidateCode(BoBGroup group, Assignment assignment, int code)
        {
            return group.ValidateCode(assignment, code);
        }*/
    }
}
