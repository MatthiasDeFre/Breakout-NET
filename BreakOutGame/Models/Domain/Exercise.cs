using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        public String Answer { get; set; }

        private String _pdf;
        public String PDF
        {
            get => Path.GetFileName(_pdf);
            set => _pdf = value;
        }

        private String _feedback;
        public String Feedback
        {
            get => Path.GetFileName(_feedback);
            set => _feedback = value;
        }
        public String Name { get; set; }
        public int Time { get; set; }
        //TO DO CATEGORY
        //
        //  WIP
        //

    }
}
