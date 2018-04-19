using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class Assignment
    {
        public int Id { get; set; }

        public int AccessCode { get; set; }

        public int ReferenceNr { get; set; }

        public int ExerciseId { get; set; }

        public int GroupOperationId { get; set; }
    }
}
