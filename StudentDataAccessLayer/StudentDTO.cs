namespace StudentDataAccessLayer
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public int grade { get; set; }


        public StudentDTO(int id, string name, int age, int grade) 
        {
            this.Id = id;
            this.Name = name;
            this.age = age;
            this.grade = grade;
        }
    }

}
