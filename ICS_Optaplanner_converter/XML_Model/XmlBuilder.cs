using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ICS_Optaplanner_converter.XML_Model
{
    public static class XmlBuilder
    {

        public static XElement MakeDay(Day day,ref int xmlId)
        {
            return new XElement("Day", new XAttribute("id", xmlId++),
                new XElement("id", day.Id),
                new XElement("middayPauzeSlot1", day.MiddayPauzeSlot1),
                new XElement("middayPauzeSlot2", day.MiddayPauzeSlot2),
                new XElement("dayIndex", day.DayIndex),
                //2017-06-04 0:00:00.0 UTC
                new XElement("date", day.Date.ToString("yyyy-MM-dd hh:mm:ss") + ".0 GMT", new XAttribute("id", xmlId++)),
                new XElement("weekend", day.Weekend),
                new XElement("dateString", day.DateString),
                new XElement("periodList", day.PeriodList, new XAttribute("id", xmlId++))
            );
        }

        public static XElement MakeCourseXml(Course course, ref int xmlId)
        {
            return new XElement("Course", new XAttribute("id", xmlId++),
                new XElement("id", course.Id),
                new XElement("urenPerDag", course.UrenPerDag),
                new XElement("code", course.Code),
                new XElement("teacher", new XAttribute("reference", course.Teacher.XmlId)),
                new XElement("lectureSize", course.LectureSize),

                new XElement("courseType", course.CourseType),
                new XElement("LectureTime", course.LectureTime),
                new XElement("minWorkingDaySize", course.MinWorkingDaySize),
                new XElement("maxWorkingDaySize", course.MaxWorkingDaySize),
                new XElement("isPCNeeded", course.IsPcNeeded),
                new XElement("firstPossibleDayIndex", course.FirstPossibleDayIndex),
                new XElement("lastPossibleDayIndex", course.LastPossibleDayIndex),
                
                new XElement("curriculumList", course.CurriculumList, new XAttribute("id", xmlId++),
                    course.CurriculumList.Select(curriculum => new XElement("Curriculum", new XAttribute("reference", curriculum.XmlId)))),
                new XElement("studentSize", course.StudentSize),
                new XElement("courseDependencies", course.CourseDependencies, new XAttribute("id", xmlId++)),
                new XElement("courseDependencyCount", course.CourseDependencyCount, new XAttribute("id", xmlId++))

            );
        }
    }
}