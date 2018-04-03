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

        public BoBSession GetById(decimal id)
        {
            return _sessions.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<BoBGroup> GetGroupsFromSession(decimal id)
        {
            return _sessions.Where(s => s.Id == id).SelectMany(s => s.Groups).Include(g => g.Students).ThenInclude(g => g.Student).OrderBy(g => g.GroupName);
     
        }
    }
}
