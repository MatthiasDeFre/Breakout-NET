using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupOperations
{
    public class PlusBehaviour : IAnswerBehaviour
    {
        public string GetAnwser(string exValue, string groupOpValue)
        {
            double anwser = Double.Parse(exValue);
            double actionValue = Double.Parse(groupOpValue);

            return  (anwser + actionValue).ToString();
        }
    }
}
