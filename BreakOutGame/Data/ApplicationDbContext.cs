using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Data.Mappers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BreakOutGame.Models;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<BoBGroup> BoBGroups { get; set; }
        public DbSet<BoBSession> BoBSessions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Normal tables
            BoBGroupConfiguration s =new BoBGroupConfiguration();

            builder.ApplyConfiguration(new BoBGroupConfiguration());
            builder.ApplyConfiguration(new BoBSessionConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new BoBActionConfiguration());
            builder.ApplyConfiguration(new PathConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new AssignmentConfiguration());
            builder.ApplyConfiguration(new GroupOperationConfiguration());
            builder.ApplyConfiguration(new StudentClassConfiguration());
            //Cross tables
            builder.ApplyConfiguration(new SessionActionConfiguration());
            builder.ApplyConfiguration(new GroupStudentConfiguration());
            builder.ApplyConfiguration(new AssignmentConfiguration());
            
        }
    }
}
