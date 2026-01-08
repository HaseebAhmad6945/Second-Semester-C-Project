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
    public class PrescriptionRepo
    {
        // CREATE
        public bool Add(Prescription prescription)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Prescriptions (AppointmentID, PatientID, DoctorID, MedicineName, Dosage, Duration, Instructions, PrescribedDate)
                  VALUES (@a, @p, @d, @m, @dos, @dur, @i, @pd); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = prescription.AppointmentID;
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = prescription.PatientID;
                cmd.Parameters.Add("@d", SqlDbType.Int).Value = prescription.DoctorID;
                cmd.Parameters.Add("@m", SqlDbType.NVarChar, 200).Value = prescription.MedicineName;
                cmd.Parameters.Add("@dos", SqlDbType.NVarChar, 100).Value = prescription.Dosage;
                cmd.Parameters.Add("@dur", SqlDbType.NVarChar, 100).Value = prescription.Duration;
                cmd.Parameters.Add("@i", SqlDbType.NVarChar).Value =
                    (object)prescription.Instructions ?? DBNull.Value;
                cmd.Parameters.Add("@pd", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    prescription.PrescriptionID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<Prescription> GetAll()
        {
            var list = new List<Prescription>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PrescriptionID, AppointmentID, PatientID, DoctorID, MedicineName, Dosage, Duration, Instructions, PrescribedDate FROM Prescriptions", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Prescription
                        {
                            PrescriptionID = r.GetInt32(0),
                            AppointmentID = r.GetInt32(1),
                            PatientID = r.GetInt32(2),
                            DoctorID = r.GetInt32(3),
                            MedicineName = r.GetString(4),
                            Dosage = r.GetString(5),
                            Duration = r.GetString(6),
                            Instructions = r.IsDBNull(7) ? null : r.GetString(7),
                            PrescribedDate = r.GetDateTime(8)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Prescription GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PrescriptionID, AppointmentID, PatientID, DoctorID, MedicineName, Dosage, Duration, Instructions, PrescribedDate FROM Prescriptions WHERE PrescriptionID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Prescription
                    {
                        PrescriptionID = r.GetInt32(0),
                        AppointmentID = r.GetInt32(1),
                        PatientID = r.GetInt32(2),
                        DoctorID = r.GetInt32(3),
                        MedicineName = r.GetString(4),
                        Dosage = r.GetString(5),
                        Duration = r.GetString(6),
                        Instructions = r.IsDBNull(7) ? null : r.GetString(7),
                        PrescribedDate = r.GetDateTime(8)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Prescription prescription)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Prescriptions 
                  SET AppointmentID = @a, PatientID = @p, DoctorID = @d, MedicineName = @m, 
                      Dosage = @dos, Duration = @dur, Instructions = @i 
                  WHERE PrescriptionID = @id", conn))
            {
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = prescription.AppointmentID;
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = prescription.PatientID;
                cmd.Parameters.Add("@d", SqlDbType.Int).Value = prescription.DoctorID;
                cmd.Parameters.Add("@m", SqlDbType.NVarChar, 200).Value = prescription.MedicineName;
                cmd.Parameters.Add("@dos", SqlDbType.NVarChar, 100).Value = prescription.Dosage;
                cmd.Parameters.Add("@dur", SqlDbType.NVarChar, 100).Value = prescription.Duration;
                cmd.Parameters.Add("@i", SqlDbType.NVarChar).Value =
                    (object)prescription.Instructions ?? DBNull.Value;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = prescription.PrescriptionID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Prescriptions WHERE PrescriptionID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Get prescriptions by PatientID
        public List<Prescription> GetByPatientId(int patientId)
        {
            var list = new List<Prescription>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PrescriptionID, AppointmentID, PatientID, DoctorID, MedicineName, Dosage, Duration, Instructions, PrescribedDate FROM Prescriptions WHERE PatientID = @pid", conn))
            {
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = patientId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Prescription
                        {
                            PrescriptionID = r.GetInt32(0),
                            AppointmentID = r.GetInt32(1),
                            PatientID = r.GetInt32(2),
                            DoctorID = r.GetInt32(3),
                            MedicineName = r.GetString(4),
                            Dosage = r.GetString(5),
                            Duration = r.GetString(6),
                            Instructions = r.IsDBNull(7) ? null : r.GetString(7),
                            PrescribedDate = r.GetDateTime(8)
                        });
                    }
                }
            }
            return list;
        }
    }
}

