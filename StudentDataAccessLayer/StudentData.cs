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


    }
}
