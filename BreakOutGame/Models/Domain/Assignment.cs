using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class Assignment
    {
        public int Id { get; set; }

        public int AccessCode { get; set; }

        public int ReferenceNr { get; set; }

        public Exercise Exercise { get; set; }
        public GroupOperation GroupOperation { get; set; }
        public AssignmentStatus Status { get; private set; }
        public int WrongCount { get; set; }

        public Boolean ValidateAnswer(String answer, Boolean areActionsEnabled)
        {
            String correctAnswer = GroupOperation.GetAnswer(Exercise.Answer);
            if (answer.Equals(correctAnswer))
            {
                if (areActionsEnabled)
                    Status = AssignmentStatus.WaitingForCode;
                else
                    Status = AssignmentStatus.Completed;
                return true;
            }
            WrongCount++;
            return false;
        }

        public Boolean ValidateCode(int code)
        {
            if (AccessCode.Equals(code))
            {
                Status = AssignmentStatus.Completed;
                return true;
            }
            return false;
        }

    }
}
