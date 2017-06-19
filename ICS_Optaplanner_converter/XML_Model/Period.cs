using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Period: XMLEntity
    {
        public Day Day { get; set; }
        public Timeslot Timeslot { get; set; }

    }
}
