using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupOperations
{
    public interface IAnswerBehaviour
    {
        String GetAnwser(String exValue, String groupOpValue);
    }
}
