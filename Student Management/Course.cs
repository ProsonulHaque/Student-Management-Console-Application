using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Student_Management
{
    [JsonObject(MemberSerialization.OptOut)]
    class Course
    {
        private string CourseID;
        private string CourseName;
        private string InstructorName;
        private int Credit;

        public string courseID { get { return CourseID; } }
        public string courseName { get { return CourseName; } }
        public string instructorName { get { return InstructorName; } }
        public int credit { get { return Credit; } }

        public Course(string courseID, string courseName, string instructorName, int credit)
        {
            this.CourseID = courseID;
            this.CourseName = courseName;
            this.InstructorName = instructorName;
            this.Credit = credit;
        }

        public void DisplayCourseName()
        {
            Console.WriteLine(this.CourseName);
        }
    }
}
