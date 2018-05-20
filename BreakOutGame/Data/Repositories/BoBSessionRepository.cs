using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Util;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace BreakOutGame.Data.Repositories
{
    public class BoBSessionRepository : IBoBSessionRepository
    {
        private DbSet<BoBSession> _sessions;
        private readonly ApplicationDbContext _dbContext;
        public BoBSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _sessions = dbContext.BoBSessions;

        }
        public IEnumerable<BoBSession> GetAll()
        {
            return _sessions.ToList();
        }

        public BoBSession GetById(int id)
        {
            return _sessions.Include(s => s.Groups).ThenInclude(g => g.Students).ThenInclude(g => g.Student).FirstOrDefault(s => s.Id == id);
        }

        public BoBSession GetByIdDetail(int id)
        {
            return _sessions
                .Include(s => s.StudentClass)
                .Include(s => s.Groups).ThenInclude(g => g.Students).ThenInclude(g => g.Student)
                .Include(s => s.Groups).ThenInclude(G => G.Path).ThenInclude(p => p.Assignments).ThenInclude(a => a.Exercise)
                .Include(s => s.Actions).ThenInclude(a => a.Action)
            .FirstOrDefault(s => s.Id == id);
        }
        public IEnumerable<BoBGroup> GetGroupsFromSession(int id)
        {
            return _sessions.Where(s => s.Id == id).SelectMany(s => s.Groups).Include(g => g.Students).ThenInclude(g => g.Student).OrderBy(g => g.GroupName);
     
        }

        public BoBAction GetAction(int sessionId, int referenceNumber)
        {
            return _sessions.Where(s => s.Id == sessionId).SelectMany(g => g.Actions).Where(a => a.OrderNumber == referenceNumber - 1).Select(a => a.Action).FirstOrDefault();
        }
        public BoBGroup GetSpecificGroupFromSession(int id, int groupId)
        {
            return _sessions.Where(s => s.Id == id).SelectMany(s => s.Groups).Include(g => g.Students)
                .ThenInclude(g => g.Student)
                .Include(g => g.Path)
                     .ThenInclude(g => g.Assignments).ThenInclude(g => g.GroupOperation)
                .Include(g => g.Path)
                     .ThenInclude(g => g.Assignments).ThenInclude(g => g.Exercise)
                .FirstOrDefault(g => g.Id == groupId);
        }

        public Assignment GetNextAssignment(int sessionId, int groupId)
        {
            return _sessions.Where(g => g.Id == sessionId).SelectMany(g => g.Groups).Where(g => g.Id == groupId).SelectMany(g => g.Path.Assignments)
                .OrderBy(g => g.ReferenceNr)
                .Include(g => g.Exercise)
                .Include(g => g.GroupOperation)
                .FirstOrDefault(g => g.Status == AssignmentStatus.NotCompleted || g.Status == AssignmentStatus.WaitingForCode);
        }

        public Student GetStudentFromSession(int sessionId, String studentId)
        {
            return _sessions.Where(s => s.Id == sessionId).Select(s => s.StudentClass).SelectMany(c => c.Students)
                .FirstOrDefault(s => s.ClassNumber.Equals(studentId));
        }

        public Boolean IsStudentInGroup(int sessionId, String studentId)
        {
            return _sessions.Where(s => s.Id == sessionId).SelectMany(s => s.Groups).SelectMany(g => g.Students)
                .Select(g => g.Student).Any(g => g.ClassNumber == studentId);
        }
        public Boolean IsGroupAuthedForAction(int sessionId, int groupId)
        {
            return true;
        }

        public SessionProgress GetCompletionPercentage(int sessionId, int groupId)
        {
            var assignments = _sessions.Where(s => s.Id == sessionId).SelectMany(g => g.Groups).Where(g => g.Id == groupId)
                .Select(g => g.Path).SelectMany(g => g.Assignments);
           return assignments.OrderBy(g => g.ReferenceNr)
                .Where(g => g.Status != AssignmentStatus.Completed).Select(g => new SessionProgress(assignments.Max(g2 => g2.ReferenceNr), g.ReferenceNr - 1)).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
