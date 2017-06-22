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

        public Period(int id, Day day, Timeslot timeslot) : base(id)
        {
            Day = day;
            Timeslot = timeslot;
        }
    }
}
