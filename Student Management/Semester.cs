using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Student_Management
{
    [JsonObject(MemberSerialization.OptOut)]
    class Semester
    {
        private string SemesterCode;
        private string Year;
        
        public string semesterCode { get { return SemesterCode; } }
        public string year { get { return Year; } }

        public Semester(string semestercode, string yr)
        {
            this.SemesterCode = semestercode;
            this.Year = yr;
        }
    }
}
