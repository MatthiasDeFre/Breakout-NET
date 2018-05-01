using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BreakOutGame.Data.Repositories
{
    public class BoBGroupRepository : IBoBGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<BoBGroup> _dbSet;
        private readonly DbSet<Assignment> _assignments;
        public BoBGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.BoBGroups;
            _assignments = _dbContext.Assignments;
        }
        public IEnumerable<BoBGroup> GetAll()
        {
            return _dbSet.Include(g => g.Students).ThenInclude(g => g.Student).OrderBy(e => e.GroupName).ToList();
        }

        public BoBGroup GetById(int id)
        {
            return _dbSet.FirstOrDefault(g => g.Id == id);
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public Assignment GetByIdWithIncludes(int pathId)
        {
            return _assignments.FirstOrDefault(a => EF.Property<int>(a, "PathId") == pathId);
        }
    }
}
