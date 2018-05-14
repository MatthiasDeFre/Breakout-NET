using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Extensions
{
    public static class GroupStatusExtension
    {
        public static string FormatString(this GroupStatus groupStatus)
        {
            switch (groupStatus)
            {
                case GroupStatus.NotSelected:
                    return "Nog niet gekozen";
                case GroupStatus.Locked:
                    return "Vergrendeld";
                case GroupStatus.Selected:
                    return "Gekozen";
                case GroupStatus.Blocked:
                    return "Geblokkeerd";
                default:
                    return null;
            }
        }
    }
}
