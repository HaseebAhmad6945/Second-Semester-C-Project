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
    public class PatientRepo
    {
        // CREATE - Add new patient
        public bool Add(Patient patient)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Patients (UserID, BloodGroup, DateOfBirth)
                  VALUES (@u, @b, @d); 
                  SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value =
                    (object)patient.UserID ?? DBNull.Value;
                cmd.Parameters.Add("@b", SqlDbType.NVarChar, 5).Value =
                    (object)patient.BloodGroup ?? DBNull.Value;
                cmd.Parameters.Add("@d", SqlDbType.Date).Value =
                    (object)patient.DateOfBirth ?? DBNull.Value;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    patient.PatientID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all patients
        public List<Patient> GetAll()
        {
            var list = new List<Patient>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PatientID, UserID, BloodGroup, DateOfBirth FROM Patients", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Patient
                        {
                            PatientID = r.GetInt32(0),
                            UserID = r.IsDBNull(1) ? (int?)null : r.GetInt32(1),
                            BloodGroup = r.IsDBNull(2) ? null : r.GetString(2),
                            DateOfBirth = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get patient by ID
        public Patient GetById(int patientId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PatientID, UserID, BloodGroup, DateOfBirth FROM Patients WHERE PatientID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = patientId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    return new Patient
                    {
                        PatientID = r.GetInt32(0),
                        UserID = r.IsDBNull(1) ? (int?)null : r.GetInt32(1),
                        BloodGroup = r.IsDBNull(2) ? null : r.GetString(2),
                        DateOfBirth = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3)
                    };
                }
            }
        }

        // READ - Get patient by UserID
        public Patient GetByUserId(int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PatientID, UserID, BloodGroup, DateOfBirth FROM Patients WHERE UserID = @uid", conn))
            {
                cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    return new Patient
                    {
                        PatientID = r.GetInt32(0),
                        UserID = r.IsDBNull(1) ? (int?)null : r.GetInt32(1),
                        BloodGroup = r.IsDBNull(2) ? null : r.GetString(2),
                        DateOfBirth = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3)
                    };
                }
            }
        }

        // UPDATE - Update patient details
        public bool Update(Patient patient)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Patients 
                  SET UserID = @u, BloodGroup = @b, DateOfBirth = @d 
                  WHERE PatientID = @id", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value =
                    (object)patient.UserID ?? DBNull.Value;
                cmd.Parameters.Add("@b", SqlDbType.NVarChar, 5).Value =
                    (object)patient.BloodGroup ?? DBNull.Value;
                cmd.Parameters.Add("@d", SqlDbType.Date).Value =
                    (object)patient.DateOfBirth ?? DBNull.Value;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = patient.PatientID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE - Delete patient by ID
        public bool Delete(int patientId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Patients WHERE PatientID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = patientId;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Search patients by blood group
        public List<Patient> SearchByBloodGroup(string bloodGroup)
        {
            var list = new List<Patient>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PatientID, UserID, BloodGroup, DateOfBirth FROM Patients WHERE BloodGroup = @bg", conn))
            {
                cmd.Parameters.Add("@bg", SqlDbType.NVarChar, 5).Value = bloodGroup;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Patient
                        {
                            PatientID = r.GetInt32(0),
                            UserID = r.IsDBNull(1) ? (int?)null : r.GetInt32(1),
                            BloodGroup = r.IsDBNull(2) ? null : r.GetString(2),
                            DateOfBirth = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3)
                        });
                    }
                }
            }
            return list;
        }
    }
}
