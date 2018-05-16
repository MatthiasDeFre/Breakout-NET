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
            builder.Property<String>("CLASSROOM_STUDENTCLASSNAME");
            builder.HasOne(s => s.StudentClass).WithMany().HasForeignKey("CLASSROOM_STUDENTCLASSNAME");
            builder.Property(s => s.BoxId).HasColumnName("BOX_ID");
            builder.Property(s => s.SessionStatus).HasColumnName("SESSIONSTATUS");
            builder.Property(s => s.AreActionsEnabled).HasColumnName("ActionsEnabled");
            builder.Property(s => s.IsFreeJoinEnabled).HasColumnName("FreeJoinEnabled");
            builder.Property(s => s.IsDistant).HasColumnName("TILE");
            builder.Property(s => s.IsFeedbackEnabled).HasColumnName("Feedback");

            //Ignore :(
            builder.Ignore(s => s.SessionState);
     
        }
    }
}
