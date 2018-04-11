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
    }
}
