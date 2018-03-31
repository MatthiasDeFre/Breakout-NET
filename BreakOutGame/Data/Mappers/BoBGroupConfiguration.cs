﻿using System;
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
      //      builder.HasMany(g => g.Students).WithOne().HasForeignKey(g => g.Id);

        }
    }
}
