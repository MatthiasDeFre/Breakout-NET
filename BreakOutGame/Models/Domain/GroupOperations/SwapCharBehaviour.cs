using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupOperations
{
    public class SwapCharBehaviour : IAnswerBehaviour
    {
        public string GetAnwser(string exValue, string groupOpValue)
        {
            char[] anwserChars = exValue.ToCharArray();
            String[] groupOpSplitted = groupOpValue.Split("&");

            char char1 = groupOpSplitted[0][0];
            char char2 = groupOpSplitted[1][0];

            var result = exValue.Select(x => x == char1 ? char2 : (x == char2 ? char1 : x)).ToArray();
           
            return new String(result);
        }
    }
}
