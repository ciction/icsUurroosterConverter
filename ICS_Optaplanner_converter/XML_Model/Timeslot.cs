using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Timeslot: XMLEntity
    {
        public int TimeSlotIndex { get; set; }


        public Timeslot(int timeSlotIndex) : base(timeSlotIndex)
        {
            TimeSlotIndex = timeSlotIndex;
        }
    }
}
