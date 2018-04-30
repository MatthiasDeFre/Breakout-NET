using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Util;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<BoBGroup> GetGroupsFromSession(int id)
        {
            return _sessions.Where(s => s.Id == id).SelectMany(s => s.Groups).Include(g => g.Students).ThenInclude(g => g.Student).OrderBy(g => g.GroupName);
     
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
