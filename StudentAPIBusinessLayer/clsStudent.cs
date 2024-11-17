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


        public clsStudent(StudentDTO SDTO, enMode sMode = enMode.AddNew)
        {
            this.ID = SDTO.Id;
            this.Name = SDTO.Name;
            this.Age = SDTO.age;
            this.Grade = SDTO.grade;
            this.Mode = sMode;
        }

        public StudentDTO SDTO  // composition   "to return it to the user"
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
        public static clsStudent Find(int id)
        {
            StudentDTO SDTO = StudentData.GetStudentByID(id);

            if (SDTO != null)
                return new clsStudent(SDTO, enMode.UpdateNew);
            else 
                return null;
        }

        private bool _AddNewStudent()
        {
            this.ID = StudentData.AddStudent(SDTO);
            return this.ID != -1;
        }

        private bool _UpdateStudent()
        {
            return StudentData.UpdateStudent(SDTO);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewStudent())
                        {
                            Mode = enMode.UpdateNew;
                            return true;
                        }
                        return false;
                    }
                case enMode.UpdateNew:
                    {
                        return _UpdateStudent();
                    }
                default: return false;
            }
        }
    }
}
