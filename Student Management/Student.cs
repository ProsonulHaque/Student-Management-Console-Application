using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Student_Management
{
    [JsonObject(MemberSerialization.OptOut)]

    //Please adjust the path/FilePath variable if doesn't work properly
    
    class Student
    {
        private string FirstName;
        private string MiddleName;
        private string LastName;
        private string StudentID;
        private Semester Batch;
        private string Department;
        private string Degree;
        public List<Semester> SemesterAttended = new List<Semester>();
        public List<Course> Courses = new List<Course>();

        public string firstName { get { return FirstName; } }
        public string middleName { get { return MiddleName; } }
        public string lastName { get { return LastName; } }
        public string studentID { get { return StudentID; } }
        public string semesterCode { get { return Batch.semesterCode; } }
        public string year { get { return Batch.year; } }
        public string department { get { return Department; } }
        public string degree { get { return Degree; } }

        public Student(string firstName, string middleName, string lastName, string studentID, string SemesterCode, string department, string degree)
        {
            this.FirstName  = firstName;
            this.MiddleName = middleName;
            this.LastName   = lastName;
            this.StudentID  = studentID;
            this.Batch      = new Semester(SemesterCode, DateTime.Now.Year.ToString());
            this.Department = department;
            this.Degree     = degree;
            this.SemesterAttended.Add(Batch);
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            string path = $@"..\\data.\{this.StudentID}.json";
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.Write(json);
                sw.Close();
            }
        }
    }
}