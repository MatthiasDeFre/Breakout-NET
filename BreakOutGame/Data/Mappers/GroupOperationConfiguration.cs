﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakOutGame.Data.Mappers
{
    public class GroupOperationConfiguration : IEntityTypeConfiguration<GroupOperation>
    {
        public void Configure(EntityTypeBuilder<GroupOperation> builder)
        {
            builder.Ignore(op => op.AnswerBehaviour);
        }
    }
}
