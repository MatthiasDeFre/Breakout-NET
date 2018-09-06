using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BreakOutGame.Models.Domain
{
    public class GroupStudent
    {
        public int BoBGroupId { get; set; }
        [JsonIgnore]
        public BoBGroup Group { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
