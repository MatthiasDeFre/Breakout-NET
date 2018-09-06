using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BreakOutGame.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public String ClassNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [JsonIgnore]
        public StudentClass StudentClass { get; set; }
        [JsonIgnore]
        public IEnumerable<GroupStudent> Groups { get; set; }
    }
}
