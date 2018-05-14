using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Extensions
{
    public static class AssignmentStatusExtension
    {
        public static string FormatString(this AssignmentStatus assignmentStatus)
        {
            switch (assignmentStatus)
            {
                case AssignmentStatus.Completed:
                    return "Voltooid";
                case AssignmentStatus.NotCompleted:
                    return "Nog niet voltooid";
                case AssignmentStatus.WaitingForCode:
                    return "Invoeren toeganscode";
              
                default:
                    return null;
            }
        }
    }
}
