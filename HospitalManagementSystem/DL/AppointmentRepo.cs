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
    public class AppointmentRepo
    {
        // CREATE
        public bool Add(Appointment appointment)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status)
                  VALUES (@p, @d, @dt, @s); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = appointment.PatientID;
                cmd.Parameters.Add("@d", SqlDbType.Int).Value = appointment.DoctorID;
                cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = appointment.AppointmentDate;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 50).Value = appointment.Status;

                conn.Open();
                object result = cmd.ExecuteScalar();
                appointment.AppointmentID = Convert.ToInt32(result);
                return true;
            }
        }

        // READ - Get all
        public List<Appointment> GetAll()
        {
            var list = new List<Appointment>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status FROM Appointments", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Appointment
                        {
                            AppointmentID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            DoctorID = r.GetInt32(2),
                            AppointmentDate = r.GetDateTime(3),
                            Status = r.GetString(4)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Appointment GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status FROM Appointments WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Appointment
                    {
                        AppointmentID = r.GetInt32(0),
                        PatientID = r.GetInt32(1),
                        DoctorID = r.GetInt32(2),
                        AppointmentDate = r.GetDateTime(3),
                        Status = r.GetString(4)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Appointment appointment)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Appointments 
                  SET PatientID = @p, DoctorID = @d, AppointmentDate = @dt, Status = @s 
                  WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = appointment.PatientID;
                cmd.Parameters.Add("@d", SqlDbType.Int).Value = appointment.DoctorID;
                cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = appointment.AppointmentDate;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 50).Value = appointment.Status;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = appointment.AppointmentID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Appointments WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Get appointments by PatientID
        public List<Appointment> GetByPatientId(int patientId)
        {
            var list = new List<Appointment>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status FROM Appointments WHERE PatientID = @pid", conn))
            {
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = patientId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Appointment
                        {
                            AppointmentID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            DoctorID = r.GetInt32(2),
                            AppointmentDate = r.GetDateTime(3),
                            Status = r.GetString(4)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get appointments by DoctorID
        public List<Appointment> GetByDoctorId(int doctorId)
        {
            var list = new List<Appointment>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status FROM Appointments WHERE DoctorID = @did", conn))
            {
                cmd.Parameters.Add("@did", SqlDbType.Int).Value = doctorId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Appointment
                        {
                            AppointmentID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            DoctorID = r.GetInt32(2),
                            AppointmentDate = r.GetDateTime(3),
                            Status = r.GetString(4)
                        });
                    }
                }
            }
            return list;
        }
    }
}

