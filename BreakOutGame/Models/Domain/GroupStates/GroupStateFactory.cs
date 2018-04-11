using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public static class GroupStateFactory
    {
        
        public static GroupState CreateState(BoBGroup group, GroupStatus status)
        {
            GroupState state = null;
            switch (status)
            {
                case GroupStatus.NotSelected:
                    state = new NotSelectedState(group);
                    break;
                case GroupStatus.Selected:
                    state = new SelectedState(group);
                    break;;
                case GroupStatus.Locked:
                    state = new LockedState(group);
                    break;
                case GroupStatus.Blocked:
                    state =new BlockedState(group);
                    break;
            }
            return state;
        }

    }
}
