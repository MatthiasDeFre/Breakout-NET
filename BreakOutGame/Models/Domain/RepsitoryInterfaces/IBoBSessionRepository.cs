using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.RepsitoryInterfaces
{
    public interface IBoBSessionRepository
    {
        IEnumerable<BoBSession> GetAll();
        BoBSession GetById(int id);
        IEnumerable<BoBGroup> GetGroupsFromSession(int id);
        BoBGroup GetSpecificGroupFromSession(int id, int groupId);
        BoBAction GetAction(int sessionId, int referenceNumber);
        Assignment GetNextAssignment(int sessionId, int groupId);
        void SaveChanges();
    }
}
