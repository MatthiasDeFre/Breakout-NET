using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupOperations
{
    public static class AnswerBehaviourFactory
    {
        public static IAnswerBehaviour CreateAnswerBehaviour(GroupOperationCategory category)
        {
            switch (category)
            {
                case GroupOperationCategory.Min:
                    return new MinBehaviour();
                case GroupOperationCategory.Multiply:
                    return new MultiplyBehaviour();
                case GroupOperationCategory.Plus:
                    return new PlusBehaviour();
                case GroupOperationCategory.Division:
                    return new DivisionBehaviour();
                case GroupOperationCategory.SwapChar:
                    return new SwapCharBehaviour();
                default:
                    return null;
                      
            }
        }
    }
}
