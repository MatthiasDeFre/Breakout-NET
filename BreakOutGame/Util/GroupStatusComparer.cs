using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Util
{
    public class GroupStatusComparer : IComparer<GroupStatus>
    {
        public int Compare(GroupStatus x, GroupStatus y)
        {
            if (x == y)
                return 0;
            if (x != GroupStatus.NotSelected)
                return 1;
            return -1;
        }
    }
}
