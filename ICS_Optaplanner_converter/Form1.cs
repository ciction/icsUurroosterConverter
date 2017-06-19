using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Ical.Net;
using Ical.Net.Interfaces.Components;
using Ical.Net.Interfaces.General;
using ICS_Optaplanner_converter.XML_Model;
using Day = ICS_Optaplanner_converter.XML_Model.Day;

namespace ICS_Optaplanner_converter
{
    public partial class Form1 : Form
    {
        private static int _xmlId = 1;
        private static readonly DateTime _startCalendarDate = new DateTime(2016, 9, 26);
        private static readonly DateTime _lastDaySemester1 = new DateTime(2017, 2, 3);
        private int _lastDayIndex = 0;

        public Form1()
        {
            InitializeComponent();
            var icsFile =
                @"C:\Users\Christophe\Documents\programming\bachelorproef\ICS_Optaplanner_converter\ICS_Optaplanner_converter\ICS_Optaplanner_converter\Resources\EHB Rooster.ics";
            var calendarCollection = Calendar.LoadFromFile(icsFile);
            var firstCalendar = calendarCollection.First();

            var events = remove_0_Duration(firstCalendar.Events);
            convertTo_eval_SmallBusiProject(events);

            //tab 1
            foreach (var @event in events)
            {
                var startdate = @event.Start.Date;
                
                //ignore semester 2 courses
                if (startdate > _lastDaySemester1) continue;

                var courseCode = DataCleaner.CleanCourseNames(@event.Summary);
                @event.Location = DataCleaner.CleanRoomNames(@event.Location);
                @event.Description = regex_filter_Singleteacher(@event.Description);
                var teacher = @event.Description;
                @event.Summary = courseCode;
                if (teacher != String.Empty)
                    dataGridView1.Rows.Add(courseCode, teacher, @event.Duration, @event.Location, @event.Start, @event.End);


                int dayindex = (int)(startdate - _startCalendarDate).TotalDays;
                if (dayindex > _lastDayIndex) _lastDayIndex = dayindex;
            }

            for (int index = 0; index < _lastDayIndex + 1; index++)
            {
                if (Database.DayList.FirstOrDefault(c => c.DayIndex == index) == null)
                {
                    
                    var day = new Day(index, _startCalendarDate.AddDays(index));
                    Database.DayList.Add(day);
                }
            }
          

            //tab 2
            var groupedEvents = events.GroupBy(e => e.Summary).Select(
                g => new
                {
                    g.Key,
                    Value = g.Sum(s => Math.Ceiling(s.Duration.TotalHours))
                }).ToList();

            foreach (var @event in groupedEvents)
            {
                var teacherCode = events.FirstOrDefault(e => e.Summary.Equals(@event.Key)).Description;
                if (teacherCode != String.Empty)
                    dataGridView2.Rows.Add(@event.Key, teacherCode, @event.Value);
            }

            //tab3
            int courseIndex = 0;
            int teacherIndex = 0;
            int teacherGroupIndex = 0;
            foreach (var @event in events)
            {
                var teacherCode = @event.Description;
                var courseCode = @event.Summary;
                var location = @event.Location;
                var startdate = @event.Start.Date;
                var startHour = @event.Start.Hour;
                var endHour = @event.End.Hour;

                //ignore semester 2 courses
                if (startdate > _lastDaySemester1) continue;


                //round duration
                var durationhours = @event.Duration.Hours;
                var durationMinutes = @event.Duration.Minutes;
                var startTimeMinutes = @event.Start.Minute;
                if (durationMinutes > 30) durationhours += 1;
                if (startTimeMinutes > 30) startHour += 1;
                endHour = startHour + durationhours;

                int dayindex = (int) (startdate - _startCalendarDate).TotalDays;

                if (teacherCode != String.Empty)
                {
                    dataGridView3.Rows.Add(courseCode, teacherCode, durationhours, @event.Location, startdate,
                        startHour, endHour, dayindex);



                    if (Database.TeacherList.FirstOrDefault(t => t.Code == teacherCode) == null)
                    {
                        var newTeacher = new Teacher(teacherIndex, teacherCode);
                        Database.TeacherList.Add(newTeacher);
                        ++teacherIndex;

                        //add groups
                        if (newTeacher.Code.Contains("_"))
                        {
                            string[] SubTeacherCodes = newTeacher.Code.Split('_');
                            foreach (var subTeacherCode in SubTeacherCodes)
                            {
                                var teacherGroup = new TeacherGroup(newTeacher.Code, subTeacherCode);
                                teacherGroup.Id = teacherGroupIndex;
                                Database.TeacherGroupList.Add(teacherGroup);
                                ++teacherGroupIndex;
                            }
                        }
                    }

                    if (Database.CoursesList.FirstOrDefault(c => c.Code == courseCode) == null)
                    {
                        //add course and link teacher
                        var newCourse = new Course(courseIndex, courseCode, Database.TeacherList.FirstOrDefault(t => t.Code == teacherCode));
                        newCourse.UrenPerDag = durationhours;
                        if (location == "DT/A.0.0011")
                        {
                            newCourse.IsPcNeeded = true;
                        }
                        Database.CoursesList.Add(newCourse);
                        ++courseIndex;

                    }
//
//
//                    var durationhours = @event.Duration.Hours;
//                    var durationMinutes = @event.Duration.Minutes;
//                    var startTimeMinutes = @event.Start.Minute;
//                    if (durationMinutes > 30) durationhours += 1;
//                    if (startTimeMinutes > 30) startHour += 1;
//                    endHour = startHour + durationhours;
//
//                    int dayindex = (int)(startdate - _startCalendarDate).TotalDays;
//                    if (dayindex > _lastDayIndex) _lastDayIndex = dayindex;


                  


//
//
                       //lecture
//                    for (int i = 0; i < durationhours; i++)
//                    {
//                        var course = new Course(courseIndex, @event.Summary, new Teacher(0));
//                        Database.CoursesList.Add(course);
//                        ++courseIndex;
//                    }
                }


            }


            CreateXML();
        }




        private string regex_filter_Singleteacher(string input)
        {
            var output = "";
            if (input == null)
                return "";
            //door Van Ryckegem Kevin\n
            var regex = @"(door(.*?)(\n))";
            //            var regex = @".*door";
            var regexResultList = Regex.Split(input, regex);


            if (regexResultList.Length > 3)

                output = regexResultList[2].Substring(1);
            else if (input == "Benoit Christophe")
                output = input;
            else
                output = String.Empty;


            return DataCleaner.ConvertTeacherName(output);
        }

        string RemoveBetween(string s, string begin, string end)
        {
            if (s == null)
                return "";
            Regex regex = new Regex(string.Format("{0}.*?{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }


        private void convertTo_eval_SmallBusiProject(List<IEvent> events)
        {
            //return events.Where(x => x.Summary.Contains("Fertile")).ToList();


            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("fertile") ||
                                                     e.Summary.ToLower().Contains("confetti") ||
                                                     e.Summary.ToLower().Contains("business model") ||
                                                     e.Summary.ToLower().Contains("prototyping") ||
                                                     e.Summary.ToLower().Contains("canvas") ||
                                                     e.Summary.ToLower().Contains("pitch")
            ).ToList())
            {
                @event.Description = "Benoit Christophe";
            }

            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("fertile")).ToList())
                @event.Summary = "EVAL_SmallBusiProject_fertile";
            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("confetti")).ToList())
                @event.Summary = "EVAL_SmallBusiProject_confetti";
            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("business model")).ToList())
                @event.Summary = "EVAL_SmallBusiProject_BM_Prototype";
            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("canvas")).ToList())
                @event.Summary = "EVAL_SmallBusiProject_canvas";
            foreach (var @event in events.Where(e => e.Summary.ToLower().Contains("pitch")).ToList())
                @event.Summary = "EVAL_SmallBusiProject_pitch";
        }

        private List<IEvent> remove_0_Duration(IUniqueComponentList<IEvent> events)
        {
            return events.Where(x => x.Duration.TotalHours > 0.5).ToList();
        }

        private void CreateXML()
        {

            var newCurriculum = new Curriculum
            {
                Id = 0,
                Code = "DigX3_Bizit",
                CoursesInCurriculum = 36
            };
            Database.CurriculumList.Add(newCurriculum);

            newCurriculum = new Curriculum
            {
                Id = 1,
                Code = "DigX3_Network",
                CoursesInCurriculum = 34
            };
            Database.CurriculumList.Add(newCurriculum);

            newCurriculum = new Curriculum
            {
                Id = 2,
                Code = "DigX3_",
                CoursesInCurriculum = 34
            };
            Database.CurriculumList.Add(newCurriculum);


            var courseSchedule = new CourseSchedule
            {
                Id = 0,
                Name = "EHB",
            };


            //new XAttribute("OrderNumber", teacher.),
            var xml = new XElement("CourseSchedule",
                new XAttribute("id", _xmlId++),
                new XElement("id", courseSchedule.Id),
                new XElement("code", courseSchedule.Name),
                new XElement("teacherList",
                    new XAttribute("id", _xmlId++),
                    Database.TeacherList.Select(teacher =>
                    {
                        teacher.XmlId = _xmlId;
                        return new XElement("Teacher",
                            new XAttribute("id", _xmlId++),
                            new XElement("id", teacher.Id),
                            new XElement("code", teacher.Code)
                        );
                    })
                ),
                new XElement("curriculumList",
                    new XAttribute("id", _xmlId++),
                    courseSchedule.Curricula.Select(curriculum =>
                        {
                            curriculum.XmlId = _xmlId;
                            return new XElement("Curriculum",
                                new XAttribute("id", _xmlId++),
                                new XElement("id", curriculum.Id),
                                new XElement("code", curriculum.Code),
                                new XElement("coursesInCurriculum", curriculum.CoursesInCurriculum)
                            );
                        }
                    )
                ),
                new XElement("courseList",
                    new XAttribute("id", _xmlId++),
                    Database.CoursesList.Select(course =>
                        {
                            course.XmlId = _xmlId;
                            //build coure structure
                            return XmlBuilder.MakeCourseXml(course, ref _xmlId);
                        }
                    )
                ),
                new XElement("courseDependencyList", new XAttribute("id", _xmlId++)),
                new XElement("teacherGroups", new XAttribute("id", _xmlId++),
                    Database.TeacherGroupList.Select(teacherGroup =>
                    {
                        teacherGroup.XmlId = _xmlId;
                        return new XElement("TeacherGroup", new XAttribute("id", _xmlId++),
                                    new XElement("id", teacherGroup.Id),
                                    new XElement("groupedTeachers", teacherGroup.Code),
                                    new XElement("individualTeacher", teacherGroup.SubCode)
                        );
                    }
                    )
                ),
                new XElement("dayList", new XAttribute("id", _xmlId++),
                    Database.DayList.Select(day =>
                        {
                            day.XmlId = _xmlId;
                           
                            if (day.Id == 0)
                            {
                                //build day structure (first)
                                return new XElement("Day", new XAttribute("id", _xmlId++));
                            }
                            else
                            {
                                //build day structure (others)
                                return XmlBuilder.MakeDay(day, ref _xmlId);
                            }
                        }
                    )
                )

            );


            // Create the XmlDocument.
            var doc = new XmlDocument();


            // Add a price element.
            //XmlElement element = doc.ReadNode(xml.CreateReader()) as XmlElement;
            doc.Load(xml.CreateReader());
            //doc.DocumentElement.AppendChild(element.in);

            //// Save the document to a file. White space is
            //// preserved (no white space).
            //doc.PreserveWhitespace = true;
            //doc.Save("data.xml");


            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<item><name>wrench</name></item>");

            // Save the document to a file and auto-indent the output.
            var writer = new XmlTextWriter("data.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
        }

    }
}