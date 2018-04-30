using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Data.Mappers
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignment");
            builder.Property<int>("SessionPathId").HasColumnName("PathId");
            builder.Property<int>("EXERCISE_ID");
            builder.Property<int>("GROUPOPERATION_ID");
            builder.HasOne(a => a.Exercise).WithMany().HasForeignKey("EXERCISE_ID");
            builder.HasOne(a => a.GroupOperation).WithMany().HasForeignKey("GROUPOPERATION_ID");

        }
    }
}
