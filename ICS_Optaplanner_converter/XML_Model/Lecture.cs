using System.Collections.Generic;
using System.Linq;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Lecture : XMLEntity
    {
        public Course Course { get; set; }
        public int LectureIndexInCourse { get; set; }
        public bool Locked { get; set; }
        public Period Period { get; set; }
        public int AbsoluteTimeslot { get; set; }
        public Room Room { get; set; }

        public Lecture(int id, Course course, bool locked, Period period, int absoluteTimeslot, Room room) : base(id)
        {
            if (Database.LectureList.FirstOrDefault(l => l.Course == course) != null)
            {
                //logic
                //1 neem zelfde course
                var sameLectures = Database.LectureList.Where(l => l.Course == course).ToList();
                //2 sorteer per index course (descending)
                var sameLecturesOrdered = sameLectures.OrderByDescending(l => l.LectureIndexInCourse).ToList();
                //3 neem de eerste (hoogste index) en tel daarop voort
                var highestIndexInCourse = sameLecturesOrdered[0].LectureIndexInCourse;
                LectureIndexInCourse = ++highestIndexInCourse;
            }
            else
            {
                LectureIndexInCourse = 0;
            }


            Course = course;
            Locked = locked;
            Period = period;
            AbsoluteTimeslot = absoluteTimeslot;
            Room = room;
        }
    }
}