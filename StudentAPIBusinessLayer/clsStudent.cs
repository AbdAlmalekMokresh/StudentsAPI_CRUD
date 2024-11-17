using StudentDataAccessLayer;
using System.Diagnostics.Contracts;

namespace StudentAPIBusinessLayer
{
    public class clsStudent
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string Name { get; set; }    
        public int Age { get; set; }
        public int Grade { get; set; }

        public StudentDTO SDTO  // composition
        {
            get { return (new StudentDTO(this.ID, this.Name, this.Age, this.Grade)); } 
        }


        public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents();
        }
        public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
    }
}
