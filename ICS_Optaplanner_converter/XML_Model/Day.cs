using System;
using System.Collections.Generic;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Day : XMLEntity
    {
        private List<Period> _periodList = new List<Period>();
        public int MiddayPauzeSlot1 { get; set; }
        public int MiddayPauzeSlot2 { get; set; }
        public int DayIndex { get; set; }
        public DateTime Date { get; set; }
        public bool Weekend { get; set; }
        public string DateString { get; set; }

        public List<Period> PeriodList
        {
            get { return _periodList; }
            set { _periodList = value; }
        }


        public Day(int dayIndex, int middayPauzeSlot1, int middayPauzeSlot2, DateTime date, bool weekend, string dateString, List<Period> periodList) : base(dayIndex)
        {
            MiddayPauzeSlot1 = 4;
            MiddayPauzeSlot2 = 5;
            DayIndex = dayIndex;
            Date = date;
            Weekend = weekend;
            DateString = dateString;
            PeriodList = periodList;

            CheckIfWeekend();
        }

        public Day(int dayIndex, DateTime date) : base(dayIndex)
        {
            DayIndex = dayIndex;
            MiddayPauzeSlot1 = 4;
            MiddayPauzeSlot2 = 5;
            DayIndex = dayIndex;
            Date = date;
            DateString = date.ToString("dd/MM/yyyy");

            CheckIfWeekend();
        }

        private void CheckIfWeekend()
        {
            if ((Date.DayOfWeek == DayOfWeek.Saturday) || (Date.DayOfWeek == DayOfWeek.Sunday))
            {
                Weekend = true;
            }

        }
    }
}