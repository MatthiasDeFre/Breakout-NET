using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class BoBActionConfiguration : IEntityTypeConfiguration<BoBAction>
    {
        public void Configure(EntityTypeBuilder<BoBAction> builder)
        {
            builder.ToTable("BOBACTION");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("ID");
            builder.Property(a => a.Action).HasColumnName("name");
            
            //      builder.HasMany(g => g.Students).WithOne().HasForeignKey(g => g.Id);

        }
    }
}
