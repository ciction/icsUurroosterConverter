using System.Collections.Generic;

namespace ICS_Optaplanner_converter.XML_Model
{
    public static class Database
    {
        public static List<Curriculum> CurriculumList { get; } = new List<Curriculum>();
        public static List<Course> CoursesList { get; } = new List<Course>();
        public static List<Teacher> TeacherList { get; } = new List<Teacher>();
        public static List<TeacherGroup> TeacherGroupList { get; } = new List<TeacherGroup>();
        public static List<Day> DayList { get; } = new List<Day>();
    }
}