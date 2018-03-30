﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public interface IBoBGroupRepository
    {
        IEnumerable<BoBGroup> GetAll();
        BoBGroup GetById(long id);
    }
}
