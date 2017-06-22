using System.Collections.Generic;

namespace ICS_Optaplanner_converter.XML_Model
{
    public static class Database
    {
        public static List<Curriculum> CurriculumList { get; } = new List<Curriculum>();
        public static List<Room> RoomList { get; } = new List<Room>();
        public static List<Course> CoursesList { get; } = new List<Course>();
        public static List<Lecture> LectureList { get; } = new List<Lecture>();
        public static List<Teacher> TeacherList { get; } = new List<Teacher>();
        public static List<TeacherGroup> TeacherGroupList { get; } = new List<TeacherGroup>();
        public static List<Day> DayList { get; } = new List<Day>();
        public static List<Timeslot> Timeslots { get; } = new List<Timeslot>();
        public static List<Period> PeriodList { get; } = new List<Period>();
    }
}