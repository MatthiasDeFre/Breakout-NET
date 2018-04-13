using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class SessionActionConfiguration : IEntityTypeConfiguration<SessionAction>
    {
        public void Configure(EntityTypeBuilder<SessionAction> builder)
        {
            builder.ToTable("BOX_BOBACTION");
            builder.Property(sa => sa.OrderNumber).HasColumnName("actions_ORDER");
            builder.Property(sa => sa.BoxId).HasColumnName("BOX_ID");
            builder.Property(sa => sa.ActionId).HasColumnName("actions_ID");
            builder
                .HasKey(t => new { BoxId = t.BoxId, ActionID = t.ActionId });
            builder
                .HasOne(pt => pt.Action)
                .WithMany(t => t.Session)
                .HasForeignKey(pt => pt.ActionId);
            builder
                .HasOne(pt => pt.Session)
                .WithMany(t => t.Actions)
                .HasForeignKey(pt => pt.BoxId);
        }
    }

}
