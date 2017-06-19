using System;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Day : XMLEntity
    {
        public int MiddayPauzeSlot1 { get; set; }
        public int MiddayPauzeSlot2 { get; set; }
        public int DayIndex { get; set; }
        public DateTime Date { get; set; }
        public bool Weekend { get; set; }
        public string DateString { get; set; }
        public Period PeriodList { get; set; }


        public Day(int dayIndex, int middayPauzeSlot1, int middayPauzeSlot2, DateTime date, bool weekend, string dateString, Period periodList) : base(dayIndex)
        {
            MiddayPauzeSlot1 = 4;
            MiddayPauzeSlot2 = 5;
            DayIndex = dayIndex;
            Date = date;
            Weekend = weekend;
            DateString = dateString;
            PeriodList = periodList;
        }

        public Day(int dayIndex, DateTime date) : base(dayIndex)
        {
            DayIndex = dayIndex;
            MiddayPauzeSlot1 = 4;
            MiddayPauzeSlot2 = 5;
            DayIndex = dayIndex;
            Date = date;
            DateString = date.ToString("dd/MM/yyyy");

        }


    }
}