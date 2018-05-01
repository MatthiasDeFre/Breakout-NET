using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Extensions;
using BreakOutGame.Models.Domain.GroupOperations;

namespace BreakOutGame.Models.Domain
{
    public class GroupOperation
    {
        public int Id { get; set; }
        public GroupOperationCategory GroupOperationCategory { get; set; }
        private IAnswerBehaviour _answerBehaviour;

        public IAnswerBehaviour AnswerBehaviour
        {
            get
            {
                if (_answerBehaviour == null)
                    _answerBehaviour = AnswerBehaviourFactory.CreateAnswerBehaviour(GroupOperationCategory);
                return _answerBehaviour;
            }
            set => _answerBehaviour = value;
        }

        public String ValueString { get; set; }

        public String AssignmentString
        {
            get
            {
                object[] values = ValueString.Split("&").ToArray<object>();
                return String.Format(GroupOperationCategory.FormatString(), values);
            }
        }

        public String GetAnswer(String exString)
        {
            return AnswerBehaviour.GetAnwser(exString, ValueString);
        }
    }

}
