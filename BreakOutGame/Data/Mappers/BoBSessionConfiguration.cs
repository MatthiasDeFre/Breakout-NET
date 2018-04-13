using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class BoBSessionConfiguration : IEntityTypeConfiguration<BoBSession>
    {
        public void Configure(EntityTypeBuilder<BoBSession> builder)
        {
            builder.ToTable("BoBSession");
            builder.HasKey(s => s.Id);
            builder.HasMany(s => s.Groups).WithOne();
            builder.Property(s => s.BoxId).HasColumnName("BOX_ID");
        }
    }
}
