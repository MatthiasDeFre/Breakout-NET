using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public class SelectedState : GroupState
    {
        public SelectedState(BoBGroup group) : base(group) { }
        //Needed for concurrency
        public override void Select()
        {
            throw new InvalidOperationException("De groep is al geselecteerd, kies aub een andere groep");
        }

        public override void Deselect()
        {
            Group.Status = GroupStatus.NotSelected;
            Group.GroupState = new NotSelectedState(Group);
        }

        public override void Lock(Boolean force)
        {
            Group.Status = GroupStatus.Locked;
            Group.GroupState = new LockedState(Group);
        }
    }
}
