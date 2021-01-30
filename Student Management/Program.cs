using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Student_Management
{
    class Program
    {
        //Please adjust the path/FilePath variable if doesn't work properly
        static string path = @"..\\data\\StudentList.txt";
        
        static void Main(string[] args)
        {   
            Console.WriteLine("....................................");
            Console.WriteLine("Welcome to Student Management System");
            Console.WriteLine("....................................\n");
            
            ShowCurrentStudents();

            ShowOption();
        }

        static void ShowCurrentStudents()
        {
            Console.WriteLine("*****CURRENT STUDENTS*****\n");
            using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
            {
                string s = "";
                int i = 1;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine($"{i}. {s}");
                    i++;
                }
                sr.Close();
            }
        }

        static void ShowOption()
        {
            Console.WriteLine("\n*****OPTIONS*****\n");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. View Student Details");
            Console.WriteLine("3. Delete Student");
            Console.WriteLine("4. Exit\n");

            Console.Write("Enter: ");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        ViewStudent();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:
                        System.Environment.Exit(0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.WriteLine("Please enter an option!");
            }
            
            ShowOption();
        }

        static void AddStudent()
        {
            Console.WriteLine("\n*****ENTER STUDENT DETAILS*****\n");

            Console.Write("First Name: "); string FirstName = Console.ReadLine();
            Console.Write("Middle Name: "); string MiddleName = Console.ReadLine();
            Console.Write("Last Name: "); string LastName = Console.ReadLine();
            Console.Write("Student ID: "); string StudentID = Console.ReadLine();
            
            string Semester = "";
            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 4) Semester = "SPR";
            else if (DateTime.Now.Month >= 5 && DateTime.Now.Month <= 8) Semester = "SUM";
            else Semester = "FAL";
            Console.WriteLine($"Semester: {Semester}");

            Console.Write("Department: "); string Department = Console.ReadLine();
            Console.Write("Degree: "); string Degree = Console.ReadLine();

            Student NewStudent = new Student(FirstName, MiddleName, LastName, StudentID, Semester, Department, Degree);
            NewStudent.Save();

            string FullName = FirstName + " " + MiddleName + " " + LastName;
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
            {
                sw.WriteLine(FullName);
                sw.Close();
            }

            Console.Write("\nCongratulations! New student added!\n");
        }

        static void ViewStudent()
        {
            Console.Write("Enter Student ID: ");
            string FileName = Console.ReadLine();
            string FilePath = @$"..\\Student Management\\data\\{FileName}.json";
            try
            {
                using (StreamReader sr = File.OpenText(FilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Student student = (Student)serializer.Deserialize(sr, typeof(Student));

                    Console.WriteLine("\n/////STUDENT DETAILS/////\n");
                    Console.WriteLine($"Name         : {student.firstName} {student.middleName} {student.lastName}");
                    Console.WriteLine($"Student ID   : {student.studentID}");
                    Console.WriteLine($"Department   : {student.department}");
                    Console.WriteLine($"Degree       : {student.degree}");
                    Console.WriteLine($"Joining Batch: {student.semesterCode} {student.year}");
                    Console.Write($"Semesters    : "); 
                    foreach(var semester in student.SemesterAttended)
                    {
                        Console.Write($"{semester.semesterCode}{semester.year} ");
                    }
                    int i = 1;
                    if (student.Courses.Count == 0) 
                    {
                        Console.Write("\nCourses      : ");
                        Console.WriteLine("N/A");
                    }
                    else
                    {
                        Console.WriteLine("\nCourses      :");
                        Console.WriteLine("\nCourse ID    Course Name     Credit\n");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"{i}. {course.courseID} {course.courseName} {course.credit}");
                            i++;
                        }
                    }

                    Console.WriteLine("\n\n*****OPTIONS*****\n");
                    Console.WriteLine("1. Add New Semester");
                    Console.WriteLine("2. Add New Course");
                    Console.WriteLine("3. Exit");
                    Console.Write("Enter: ");
                    try
                    {
                        int option = Convert.ToInt32(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                AddSemester(student);
                                break;
                            case 2:
                                AddCourse(student);
                                break;
                            case 3:
                                System.Environment.Exit(0);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    sr.Close();
                }
            }
            catch
            {
                Console.WriteLine("Invalid Student ID!");
            }
        }

        static void AddSemester(Student student)
        {
            Console.WriteLine("\n*****CHOOSE SEMESTER*****\n");
            Console.WriteLine("1. SPR");
            Console.WriteLine("2. SMR");
            Console.WriteLine("3. FAL");
            Console.Write("Enter: ");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                string semester = "";
                switch (option)
                {
                    case 1:
                        semester = "SPR";
                        break;
                    case 2:
                        semester = "SMR";
                        break;
                    case 3:
                        semester = "FAL";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                student.SemesterAttended.Add(new Semester(semester, DateTime.Now.Year.ToString()));
                Save(student);
                Console.WriteLine("\nCongratulations! New Semester Added!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.WriteLine("Please enter an option!");
            }
        }

        static void AddCourse(Student student)
        {
            List<Course> CourseList = new List<Course>()
                {
                    new Course("MAT 101", "Mathematics", "John Doe", 4),
                    new Course("PHY 201", "Physics", "Mohn Doe", 3),
                    new Course("CSE 110", "Computer Fundamentals", "Rohn Doe", 4),
                    new Course("ENG 301", "English Language", "Kahn Doe", 2),
                    new Course("DLD 221", "Digital Logic Design", "Hahn Doe", 4),
                    new Course("EMF 132", "Electromagnetic Field", "Vohn Doe", 3)
                };

            Console.WriteLine("\n*****CHOOSE A COURSE TO ADD*****\n");
            int i = 1;
            foreach (var course in CourseList)
            {
                Console.WriteLine($"{i}. {course.courseName}");
                i++;
            }
            Console.Write("Enter: ");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                if (option < 1 || option > 6) throw new ArgumentOutOfRangeException();
                student.Courses.Add(CourseList[option - 1]);
                student.Save();
                Console.WriteLine("\nCongratulations! New course added!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.WriteLine("Please enter an option!");
            }
        }

        static void Save(Student student)
        {
            string json = JsonConvert.SerializeObject(student, Formatting.Indented);
            string FilePath = $@"..\\data\\{student.studentID}.json";
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(FilePath))
            {
                sw.Write(json);
                sw.Close();
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Student ID: ");
            string FileName = Console.ReadLine();
            string FilePath = @$"..\\data\\{FileName}.json";
            string name = "";
            try
            {
                using (StreamReader sr = File.OpenText(FilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Student student = (Student)serializer.Deserialize(sr, typeof(Student));
                    name = student.firstName + " " + student.middleName + " " + student.lastName;
                    sr.Close();
                }
            }
            catch
            {
                Console.WriteLine("Invalid Student ID!");
                return;
            }

            List<string> StudentList = new List<string>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s= sr.ReadLine())!= null)
                {
                    StudentList.Add(s);
                }
                sr.Close();
            }
            System.IO.File.Delete(path);
            using (StreamWriter sw = File.AppendText(path))
            {
                foreach(string line in StudentList)
                {
                    if (line == name) continue;
                    sw.WriteLine(line);
                }
                sw.Close();
            }
            System.IO.File.Delete(FilePath);
        }
    }
}
