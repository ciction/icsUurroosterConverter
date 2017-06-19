using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Curriculum: XMLEntity
    {
        public string Code { get; set; }
        public int CoursesInCurriculum { get; set; }

        public Curriculum(int id) : base(id)
        {
            Id = id;
        }

        public Curriculum() : base()
        {
        }
    }
}
