using System.Collections.Generic;
using System.Linq;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Course : XMLEntity
    {
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

        public List<Curriculum> CurriculumList { get; set; }
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
            this.LastPossibleDayIndex = int.MaxValue;

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


        private void SetCurricula()
        {

            if (Code.ToLower().Contains("abap"))
            {
                CurriculumList.Add(Database.CurriculumList.SingleOrDefault(c => c.Code == "DigX3_Bizit"));
            }
            else
            {
                CurriculumList.AddRange(Database.CurriculumList);
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
            LastPossibleDayIndex = lastPossibleDayIndex;

            this.CurriculumList = new List<Curriculum>();
            SetStudentSize();
            SetCurricula();
            this.CourseDependencies = 0;
            this.CourseDependencyCount = 0;
        }
    }
}