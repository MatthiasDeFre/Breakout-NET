using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.Property(e => e.FirstName).HasColumnName("FIRSTNAME");
            builder.Property(e => e.LastName).HasColumnName("LASTNAME");
            builder.Property(e => e.ClassNumber).HasColumnName("CLASSNUMBER");
           builder.Property<String>("STUDENTCLASS_STUDENTCLASSNAME");
            builder.HasOne(s => s.StudentClass).WithMany(s => s.Students).HasForeignKey("STUDENTCLASS_STUDENTCLASSNAME");

        }
    }
}
