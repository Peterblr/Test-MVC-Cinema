using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        public int GradeValue { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
