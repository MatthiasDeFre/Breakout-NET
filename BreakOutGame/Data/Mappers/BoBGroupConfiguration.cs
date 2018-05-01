using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class BoBGroupConfiguration : IEntityTypeConfiguration<BoBGroup>
    {
        public void Configure(EntityTypeBuilder<BoBGroup> builder)
        {
            builder.ToTable("BOBGROUP");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasColumnName("ID");
            builder.Property(g => g.GroupName).HasColumnName("name");
            builder.Property(g => g.Status).HasDefaultValue(GroupStatus.NotSelected);
            
            //      builder.HasMany(g => g.Students).WithOne().HasForeignKey(g => g.Id);
            //builder.Property(g => g.PathId).HasColumnName("PATH_ID");

            builder.Property<int>("PathId").HasColumnName("PATH_ID");

            builder.HasOne(g => g.Path).WithOne();

            builder.Ignore(g => g.GroupState);
            builder.Ignore(g => g.NextAssignment);
        }
    }
}
