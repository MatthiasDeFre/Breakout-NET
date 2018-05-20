using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public class BlockedState : GroupState
    {
        public BlockedState(BoBGroup group) : base(group) { }
      
        public override void Block()
        {
            //throw new InvalidOperationException("Deze groep is al geblokkeerd");
        }

        public override void Deblock()
        {
            Group.Status = GroupStatus.Locked;
            Group.GroupState = new LockedState(Group);
        }
    }
}
