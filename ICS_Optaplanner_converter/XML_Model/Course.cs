using System;
using System.Collections.Generic;
using System.Linq;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Course : XMLEntity
    {
        private List<Curriculum> _curriculumList = new List<Curriculum>();
        public string Code { get; set; }
        public int UrenPerDag { get; set; }
        public Teacher Teacher { get; set; }
        public int LectureSize { get; set; }
        public string CourseType { get; set; }
        public int LectureTime { get; set; }
        public int MinWorkingDaySize { get; set; }
        public int MaxWorkingDaySize { get; set; }
        public bool IsPcNeeded { get; set; }
        public int FirstPossibleDayIndex { get; set; }
        public int LastPossibleDayIndex { get; set; }

        public List<Curriculum> CurriculumList
        {
            get { return _curriculumList; }
            set { _curriculumList = value; }
        }

        public int StudentSize { get; set; }
        public int CourseDependencies { get; set; }
        public int CourseDependencyCount { get; set; }
        
            



        public Course(int id, string code, Teacher teacher) : base(id)
        {
            this.Code = code;
            this.UrenPerDag = 2;
            this.Teacher = teacher;
            this.LectureSize = 3;
            SetCourseType();
            this.LectureTime = 0;
            this.MinWorkingDaySize = 1;
            this.MaxWorkingDaySize = int.MaxValue;
            this.IsPcNeeded = false;
            this.FirstPossibleDayIndex = -1;
            this.SetLastPossibleDayIndex();

            this.CurriculumList = new List<Curriculum>();
            SetStudentSize();
            SetCurricula();
            this.CourseDependencies = 0;
            this.CourseDependencyCount = 0;
        }

        private void SetCourseType()
        {
            this.CourseType = "Hoorcollege";

            if (Code.EndsWith("_WK"))
            {
                this.CourseType = "Werkcollege";
                this.UrenPerDag = 3;
            }
        }

        private void SetStudentSize()
        {
            StudentSize = 25;

            switch (Code)
            {
                case "Infosessie_PDT_trajecten__WK":
                    StudentSize = 100; break;
//                case "Infosessie_PDT_trajecten__WK":
//                    StudentSize = 100; break;
                default:
                    break;
            }

            //IT trends and talents
            if (Code.StartsWith("EVAL_"))
                StudentSize = 4;
            if (Code.Contains("Final_Work"))
                StudentSize = 50;
        }

        public void SetLastPossibleDayIndex()
        {
            DateTime startCalendarDate = new DateTime(2016, 9, 26);
            DateTime LastDaySemester1BeforeExams = new DateTime(2016, 12, 23);


            if (Code.ToLower().Contains("infosessie") ||
                Code.ToLower().Contains("kaai") ||
                Code.ToLower().Contains("trends"))
            {
                LastPossibleDayIndex = 0;
            }
            else
            {
                LastPossibleDayIndex = (int)
                    (LastDaySemester1BeforeExams - startCalendarDate).TotalDays;
            }
        }

        private void SetCurricula()
        {

            if (Code.ToLower().Contains("infosessie") ||
                Code.ToLower().Contains("trends") ||
                Code.ToLower().Contains("final") ||
                Code.ToLower().Contains("business") ||
                Code.ToLower().Contains("kick-off") ||
                Code.ToLower().Contains("internship"))
            {
                CurriculumList.AddRange(Database.CurriculumList);
            }
            else if (Code.ToLower().Contains("java") ||
                     Code.ToLower().Contains("database") ||
                     Code.ToLower().Contains("mobile"))
            {
                CurriculumList.Add(Database.CurriculumList.SingleOrDefault(c => c.Code == "DigX3_Bizit"));
                CurriculumList.Add(Database.CurriculumList.SingleOrDefault(c => c.Code == "DigX3_Network"));
            }
            else
            {
                CurriculumList.Add(Database.CurriculumList.SingleOrDefault(c => c.Code == "DigX3_Bizit"));
            }
        }

        public Course(int xmlId, int id, string code, int urenPerDag, Teacher teacher, int lectureSize, string courseType, int lectureTime, int minWorkingDaySize, int maxWorkingDaySize, bool isPcNeeded, int firstPossibleDayIndex, int lastPossibleDayIndex) : base(id)
        {
            Code = code;
            UrenPerDag = urenPerDag;
            Teacher = teacher;
            LectureSize = lectureSize;
            CourseType = courseType;
            LectureTime = lectureTime;
            MinWorkingDaySize = minWorkingDaySize;
            MaxWorkingDaySize = maxWorkingDaySize;
            IsPcNeeded = isPcNeeded;
            FirstPossibleDayIndex = firstPossibleDayIndex;
            this.SetLastPossibleDayIndex();


            this.CurriculumList = new List<Curriculum>();
            SetStudentSize();
            SetCurricula();
            this.CourseDependencies = 0;
            this.CourseDependencyCount = 0;
        }
    }
}