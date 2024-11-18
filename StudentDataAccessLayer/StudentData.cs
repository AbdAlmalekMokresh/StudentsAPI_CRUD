using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

namespace StudentDataAccessLayer
{
    public class StudentData
    {
        static string _connectionString = "Server=localhost; Database=StudentsDB; User Id=sa; Password=0000; TrustServerCertificate=true;";

        public static List<StudentDTO> GetAllStudents()
        {
            var StudentList = new List<StudentDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                              ));
                        }
                    }
                }
                return StudentList;
            }

        }


        public static List<StudentDTO> GetPassedStudents()
        {
            var StudentList = new List<StudentDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                              ));
                        }
                    }
                }
                return StudentList;
            }
        }


        public static double GetAverageGrade()
        {
            double avgGrade = 0;
            using (SqlConnection connectoin = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAverageGrade", connectoin))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connectoin.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        avgGrade = Convert.ToDouble(result);
                    }
                }
            }
            return avgGrade;
        }



        public static StudentDTO GetStudentByID(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                        }
                        else return null;
                    }
                }
            }
        }


        public static int AddStudent(StudentDTO studentDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_AddStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", studentDTO.Name);
                    command.Parameters.AddWithValue("@Age", studentDTO.age);
                    command.Parameters.AddWithValue("@Grade", studentDTO.grade);

                    var outputIDParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIDParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputIDParam.Value;

                }
            }
        }

        public static bool UpdateStudent(StudentDTO studentDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentId", studentDTO.Id);
                    command.Parameters.AddWithValue("@Name", studentDTO.Name);
                    command.Parameters.AddWithValue("@Age", studentDTO.age);
                    command.Parameters.AddWithValue("@Grade", studentDTO.grade);

                    connection.Open();
                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                        return true;
                    else return false;
                }
            }
        }



        public static bool DeleteStudent(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentId", id);

                    connection.Open();
                    int res = (int)command.ExecuteScalar();
                    return res == 1;
                }
            }
        }
    }
}
