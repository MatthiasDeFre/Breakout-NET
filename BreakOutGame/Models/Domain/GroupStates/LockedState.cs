using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public class LockedState : GroupState
    {

        public LockedState(BoBGroup group) : base(group) { }

        public override void Block()
        {
            Group.Status = GroupStatus.Blocked;
            Group.GroupState  =new BlockedState(Group);
        }
        public override bool ValidateAnswer(Assignment assignment, string answer, Boolean actionsEnabled, Boolean blockingEnabled)
        {
            bool correct = assignment.ValidateAnswer(answer, actionsEnabled);
            if (assignment.WrongCount == 3 && blockingEnabled)
            {
                Block();
            }
            return correct;
        }

        public override bool ValidateCode(Assignment assignment, int code)
        {
            return assignment.ValidateCode(code);
        }
    }
}
