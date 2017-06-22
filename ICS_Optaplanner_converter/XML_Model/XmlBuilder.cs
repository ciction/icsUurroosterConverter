using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ICS_Optaplanner_converter.XML_Model
{
    public static class XmlBuilder
    {

        public static XElement MakeFristDay(Day day, ref int xmlId)
        {
            int newId = xmlId + 1;
            XElement xElement = new XElement("Day", new XAttribute("id", xmlId++),
                new XElement("id", day.Id),
                new XElement("middayPauzeSlot1", day.MiddayPauzeSlot1),
                new XElement("middayPauzeSlot2", day.MiddayPauzeSlot2),
                new XElement("dayIndex", day.DayIndex),
                //2017-06-04 0:00:00.0 UTC
                new XElement("date", day.Date.ToString("yyyy-MM-dd hh:mm:ss") + ".0 GMT",
                    new XAttribute("id", xmlId++)),
                new XElement("weekend", day.Weekend),
                new XElement("dateString", day.DateString),
                new XElement("periodList", new XAttribute("id", xmlId++),
                    day.PeriodList.Select(p =>
                    {
                        newId += 2;
                        return MakeFirstPeriodXml(p, newId);
                    }))
            );
            xmlId += (20);
            return xElement;
        }

        public static XElement MakeNextDay(Day day, ref int xmlId)
        {
            int newId = xmlId + 2;

            XElement xElement = new XElement("Day", new XAttribute("id", xmlId++),
                new XElement("id", day.Id),
                new XElement("middayPauzeSlot1", day.MiddayPauzeSlot1),
                new XElement("middayPauzeSlot2", day.MiddayPauzeSlot2),
                new XElement("dayIndex", day.DayIndex),
                //2017-06-04 0:00:00.0 UTC
                new XElement("date", day.Date.ToString("yyyy-MM-dd hh:mm:ss") + ".0 GMT",
                    new XAttribute("id", xmlId++)),
                new XElement("weekend", day.Weekend),
                new XElement("dateString", day.DateString),
                new XElement("periodList", new XAttribute("id", xmlId++),
                    day.PeriodList.Select(p =>
                    {
                        newId += 1;
                        return MakeNextPeriodXml(p, newId);
                    }))
            );
            xmlId += 10;
            return xElement;
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

                new XElement("curriculumList", new XAttribute("id", xmlId++),
                    course.CurriculumList.Select(
                        curriculum => new XElement("Curriculum", new XAttribute("reference", curriculum.XmlId)))),
                new XElement("studentSize", course.StudentSize),
                new XElement("courseDependencies", new XAttribute("id", xmlId++)),
                new XElement("courseDependencyCount", new XAttribute("id", xmlId++))

            );
        }

        public static XElement MakeFirstPeriodXml(Period period, int xmlId)
        {
            period.XmlId = xmlId;
            period.Timeslot.XmlId = xmlId + 1;
            return new XElement("Period", new XAttribute("id", xmlId++),
                new XElement("id", period.Id),
                new XElement("day", new XAttribute("reference", period.Day.XmlId)),
                new XElement("timeslot", new XAttribute("id", xmlId++),
                    new XElement("id", period.Timeslot.TimeSlotIndex),
                    new XElement("timeslotIndex", period.Timeslot.TimeSlotIndex)
                )
            );
        }

        public static XElement MakeNextPeriodXml(Period period, int xmlId)
        {
            period.XmlId = xmlId;
            return new XElement("Period", new XAttribute("id", xmlId++),
                new XElement("id", period.Id),
                new XElement("day", new XAttribute("reference", period.Day.XmlId)),
                new XElement("timeslot", new XAttribute("reference",
                    Database.DayList[0].PeriodList[period.Timeslot.TimeSlotIndex].Timeslot.XmlId))
            );
        }

        public static XElement MakeRoomXML(Room room, int xmlId)
        {
            room.XmlId = xmlId;
            return new XElement("Room", new XAttribute("id", room.XmlId),
                    new XElement("id", room.Id),
                    new XElement("code", room.Code),
                    new XElement("capacity", room.Capacity),
                    new XElement("pcCount", room.PcCount)
                );
        }

        public static XElement MakeLectureXML(Lecture lecture, int xmlId)
        {
            lecture.XmlId = xmlId;
            return new XElement("Lecture", new XAttribute("id", lecture.XmlId++),
                new XElement("id", lecture.Id),
                new XElement("course", new XAttribute("reference", lecture.Course.XmlId)),
                new XElement("lectureIndexInCourse", lecture.LectureIndexInCourse),
                new XElement("locked", lecture.Locked),
                new XElement("period", new XAttribute("reference", lecture.Period.XmlId)),
                new XElement("absoluteTimeslot", lecture.AbsoluteTimeslot),
                new XElement("room", new XAttribute("reference", lecture.Room.XmlId))
                );
        }
    }
}