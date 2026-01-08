using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HospitalManagementSystem.BL;

namespace HospitalManagementSystem.DL
{
    public class DoctorRepo
    {
        // CREATE - Add new doctor
        public bool Add(Doctor doctor)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Doctors (UserID, Specialization)
                  VALUES (@u, @s); 
                  SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = doctor.UserID;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 100).Value = doctor.Specialization;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    doctor.DoctorID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all doctors
        public List<Doctor> GetAll()
        {
            var list = new List<Doctor>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DoctorID, UserID, Specialization FROM Doctors", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Doctor
                        {
                            DoctorID = r.GetInt32(0),
                            UserID = r.GetInt32(1),
                            Specialization = r.GetString(2)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get doctor by ID
        public Doctor GetById(int doctorId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DoctorID, UserID, Specialization FROM Doctors WHERE DoctorID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = doctorId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    return new Doctor
                    {
                        DoctorID = r.GetInt32(0),
                        UserID = r.GetInt32(1),
                        Specialization = r.GetString(2)
                    };
                }
            }
        }

        // READ - Get doctor by UserID
        public Doctor GetByUserId(int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DoctorID, UserID, Specialization FROM Doctors WHERE UserID = @uid", conn))
            {
                cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    return new Doctor
                    {
                        DoctorID = r.GetInt32(0),
                        UserID = r.GetInt32(1),
                        Specialization = r.GetString(2)
                    };
                }
            }
        }

        // UPDATE - Update doctor details
        public bool Update(Doctor doctor)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Doctors 
                  SET UserID = @u, Specialization = @s 
                  WHERE DoctorID = @id", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = doctor.UserID;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 100).Value = doctor.Specialization;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = doctor.DoctorID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE - Delete doctor by ID
        public bool Delete(int doctorId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Doctors WHERE DoctorID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = doctorId;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Search doctors by specialization
        public List<Doctor> SearchBySpecialization(string specialization)
        {
            var list = new List<Doctor>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DoctorID, UserID, Specialization FROM Doctors WHERE Specialization LIKE @spec", conn))
            {
                cmd.Parameters.Add("@spec", SqlDbType.NVarChar, 100).Value = "%" + specialization + "%";
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Doctor
                        {
                            DoctorID = r.GetInt32(0),
                            UserID = r.GetInt32(1),
                            Specialization = r.GetString(2)
                        });
                    }
                }
            }
            return list;
        }
    }
}
