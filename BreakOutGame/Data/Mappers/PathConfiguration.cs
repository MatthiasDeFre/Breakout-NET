using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class PathConfiguration : IEntityTypeConfiguration<SessionPath>
    {
        public void Configure(EntityTypeBuilder<SessionPath> builder)
        {
           
            builder.HasMany(p => p.Assignments).WithOne();
        }
    }
}
