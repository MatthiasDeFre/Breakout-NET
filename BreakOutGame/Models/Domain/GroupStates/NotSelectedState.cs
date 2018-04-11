using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public class NotSelectedState : GroupState
    {
        public NotSelectedState(BoBGroup group) : base(group) { }

        public override void Select()
        {
            Group.Status = GroupStatus.Selected;
            Group.GroupState = new SelectedState(Group);
        }

        public override void Lock(bool force)
        {
            if (force)
            {
                Group.Status = GroupStatus.Locked;
                Group.GroupState = new LockedState(Group);
            }
            else
            {
                throw new InvalidOperationException("Groep is nog niet gekozen en kan dus niet vergrendeld worden");
            }
        }
    }
}
