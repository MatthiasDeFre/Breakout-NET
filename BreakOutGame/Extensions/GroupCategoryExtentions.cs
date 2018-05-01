using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain.GroupOperations;

namespace BreakOutGame.Extensions
{
    public static class GroupCategoryExtentions
    {
        public static string FormatString(this GroupOperationCategory groupOperation)
        {
            switch (groupOperation)
            {
                case GroupOperationCategory.Division:
                    return "Deel de uitkomst met {0}";
                case GroupOperationCategory.Min:
                    return "Verminder de uitkomst met {0}";
                case GroupOperationCategory.Multiply:
                    return "Vermenigvuldig de uitkomst met {0}";
                case GroupOperationCategory.Plus:
                    return "Vermeerder de uitkomst met {0}";
                case GroupOperationCategory.SwapChar:
                    return "Verwissel letter {0} met {1}";
                default:
                    return null;
            }
        }
    }
}
