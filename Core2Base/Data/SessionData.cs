using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Data
{
    public class SessionData : Data
    {

    }
}


/*
 public class CourseData : Data
{
    public static List<Course> GetAllCourses()
    {
        List<Course> courses = new List<Course>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string sql = @"SELECT Course.Id, Course.Code, Course.Name as CourseName
                                FROM Course";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Course course = new Course()
                {
                    Id = (int)reader["Id"],
                    Code = (string)reader["Code"],
                    Name = (string)reader["CourseName"]
                };
                courses.Add(course);
            }
        }

        return courses;
    }

*/