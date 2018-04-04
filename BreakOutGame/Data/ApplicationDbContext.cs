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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.Entity<GroupStudent>().ToTable("BOBGROUP_Student");
            builder.Entity<GroupStudent>()
                .HasKey(t => new { GroupId = t.BoBGroup_ID, StudentId = t.students_ID});
            builder.Entity<GroupStudent>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.Groups)
                .HasForeignKey(pt => pt.students_ID);

            builder.Entity<GroupStudent>()
                .HasOne(pt => pt.Group)
                .WithMany(t => t.Students)
                .HasForeignKey(pt => pt.BoBGroup_ID);
      
            builder.ApplyConfiguration(new BoBGroupConfiguration());
            builder.ApplyConfiguration(new BoBSessionConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}
