
using System;
using System.Data.SqlClient;
using Tutorial_5.DTOs.Requests;
using Tutorial_5.Exceptions;

namespace Tutorial_5.Services
{
    public class SqlServerStudentDbService : IStudentServiceDb
    {

        private string connectionString = "Data Source=db-mssql;Initial Catalog=s19022;Integrated Security=True";

        public void EnrollStudent(EnrollStudentRequest request)
        {
            //4. Check if index does not exists -> INSERT/400
            //5. return Enrollment model
            using (var con = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                
                command.CommandText = "SELECT * FROM Studies WHERE Name=@Name";
                command.Parameters.AddWithValue("Name", request.Studies);
                command.Connection = con;
                con.Open();

                var dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    throw new StudiesNotFoundException();
                }
                int idStudies = (int)dr["IdStudy"];

                command.CommandText = "SELECT idEnrollment FROM Enrollment e1 WHERE e1.Semester=1 " +
                    "AND e1.IdStudy=@IdStud and e1.startDate = (select max(e2.StartDate) from enrollment e2  WHERE e2.Semester=1 AND e2.IdStudy=@IdStud)";

                command.Parameters.AddWithValue("IdStud", idStudies);

                dr.Close();

                dr = command.ExecuteReader();

                var tran = con.BeginTransaction();

                if (!dr.Read())
                {
                    try {
                        command.CommandText = "insert into enrollment(IdEnrollment, Semester, IdStudy, StartDate) values @IdEnrollment, 1, IdStudy, getdate()";
                        command.Parameters.AddWithValue("IdEnrollment", GetEnrollmentId());
                        command.Parameters.AddWithValue("IdStudy", idStudies);
                    } catch (Exception ex)
                    { 
                        tran.Rollback();
                    }
                }


                
              /*  //4. ....

                //x.. INSERT Student
                command.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName) VALUES (@FirstName, @LastName, .....";
                //...
                command.Parameters.AddWithValue("FistName", request.FirstName);
                //...
                command.ExecuteNonQuery();

                tran.Commit(); //make all the changes in db visible to another users

                ///tran.Rollback();
            */}


        }

        private int GetEnrollmentId()
        {
            int id = 1;
            using (var con = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.CommandText = "SELECT max(idEnrollment) FROM enrollment";
                command.Connection = con;
                con.Open();

                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    id = (int)reader["idEnrollment"];
                }
                return id;

            }
        }

            public void PromoteStudents(int semester, string studies)
        {
            
        }
    }
}
