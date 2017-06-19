using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class CourseSchedule : XMLEntity
    {
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Curriculum> Curricula { get; set; }
        public List<Course> Courses { get; set; }

        public CourseSchedule(int id) : base(id)
        {
            Id = id;
        }

        public CourseSchedule()
        {
        }
    }
}
