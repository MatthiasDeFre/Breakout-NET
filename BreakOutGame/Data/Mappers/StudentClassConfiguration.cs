using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            builder.ToTable("STUDENTCLASS");
            builder.HasKey(c => c.ClassName);
           // builder.Property<String>("STUDENTCLASS_STUDENTCLASSNAME");
            builder.HasMany(c => c.Students).WithOne(c => c.StudentClass);//.HasForeignKey("STUDENTCLASS_STUDENTCLASSNAME");
     
            builder.Property(c => c.ClassName).HasColumnName("STUDENTCLASSNAME");
        }
    }
}
