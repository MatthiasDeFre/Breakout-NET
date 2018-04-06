﻿using System;
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

        public BoBGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.BoBGroups;
        }
        public IEnumerable<BoBGroup> GetAll()
        {
            return _dbSet.Include(g => g.Students).ThenInclude(g => g.Student).OrderBy(e => e.GroupName).ToList();
        }

        public BoBGroup GetById(int id)
        {
            return _dbSet.FirstOrDefault(g => g.Id == id);
        }
    }
}
