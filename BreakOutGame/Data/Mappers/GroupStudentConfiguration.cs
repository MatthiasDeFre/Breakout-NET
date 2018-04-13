using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class GroupStudentConfiguration : IEntityTypeConfiguration<GroupStudent>
    {
        public void Configure(EntityTypeBuilder<GroupStudent> builder)
        {
            builder.ToTable("BOBGROUP_Student");
            builder.Property(gs => gs.BoBGroupId).HasColumnName("BoBGroup_ID");
            builder.Property(gs => gs.StudentId).HasColumnName("students_ID");
            builder
                .HasKey(t => new { GroupId = t.BoBGroupId, StudentId = t.StudentId });
            builder
                .HasOne(pt => pt.Student)
                .WithMany(p => p.Groups)
                .HasForeignKey(pt => pt.StudentId);
            builder
                .HasOne(pt => pt.Group)
                .WithMany(t => t.Students)
                .HasForeignKey(pt => pt.BoBGroupId);

        }
    }


}
