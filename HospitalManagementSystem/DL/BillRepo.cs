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
    public class BillRepo
    {
        // CREATE
        public bool Add(Bill bill)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Bills (PatientID, AppointmentID, TotalAmount, PaidAmount, PaymentStatus, PaymentMethod, BillingDate)
                  VALUES (@p, @a, @t, @pd, @ps, @pm, @bd); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = bill.PatientID;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = (object)bill.AppointmentID ?? DBNull.Value;
                cmd.Parameters.Add("@t", SqlDbType.Decimal).Value = bill.TotalAmount;
                cmd.Parameters.Add("@pd", SqlDbType.Decimal).Value = bill.PaidAmount;
                cmd.Parameters.Add("@ps", SqlDbType.NVarChar, 20).Value = bill.PaymentStatus;
                cmd.Parameters.Add("@pm", SqlDbType.NVarChar, 50).Value =
                    (object)bill.PaymentMethod ?? DBNull.Value;
                cmd.Parameters.Add("@bd", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    bill.BillID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<Bill> GetAll()
        {
            var list = new List<Bill>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT BillID, PatientID, AppointmentID, TotalAmount, PaidAmount, PaymentStatus, PaymentMethod, BillingDate FROM Bills", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Bill
                        {
                            BillID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            AppointmentID = r.IsDBNull(2) ? (int?)null : r.GetInt32(2),
                            TotalAmount = r.GetDecimal(3),
                            PaidAmount = r.GetDecimal(4),
                            PaymentStatus = r.GetString(5),
                            PaymentMethod = r.IsDBNull(6) ? null : r.GetString(6),
                            BillingDate = r.GetDateTime(7)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Bill GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT BillID, PatientID, AppointmentID, TotalAmount, PaidAmount, PaymentStatus, PaymentMethod, BillingDate FROM Bills WHERE BillID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Bill
                    {
                        BillID = r.GetInt32(0),
                        PatientID = r.GetInt32(1),
                        AppointmentID = r.IsDBNull(2) ? (int?)null : r.GetInt32(2),
                        TotalAmount = r.GetDecimal(3),
                        PaidAmount = r.GetDecimal(4),
                        PaymentStatus = r.GetString(5),
                        PaymentMethod = r.IsDBNull(6) ? null : r.GetString(6),
                        BillingDate = r.GetDateTime(7)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Bill bill)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Bills 
                  SET PatientID = @p, AppointmentID = @a, TotalAmount = @t, PaidAmount = @pd, 
                      PaymentStatus = @ps, PaymentMethod = @pm 
                  WHERE BillID = @id", conn))
            {
                cmd.Parameters.Add("@p", SqlDbType.Int).Value = bill.PatientID;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = (object)bill.AppointmentID ?? DBNull.Value;
                cmd.Parameters.Add("@t", SqlDbType.Decimal).Value = bill.TotalAmount;
                cmd.Parameters.Add("@pd", SqlDbType.Decimal).Value = bill.PaidAmount;
                cmd.Parameters.Add("@ps", SqlDbType.NVarChar, 20).Value = bill.PaymentStatus;
                cmd.Parameters.Add("@pm", SqlDbType.NVarChar, 50).Value =
                    (object)bill.PaymentMethod ?? DBNull.Value;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = bill.BillID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Bills WHERE BillID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Get bills by PatientID
        public List<Bill> GetByPatientId(int patientId)
        {
            var list = new List<Bill>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT BillID, PatientID, AppointmentID, TotalAmount, PaidAmount, PaymentStatus, PaymentMethod, BillingDate FROM Bills WHERE PatientID = @pid", conn))
            {
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = patientId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Bill
                        {
                            BillID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            AppointmentID = r.IsDBNull(2) ? (int?)null : r.GetInt32(2),
                            TotalAmount = r.GetDecimal(3),
                            PaidAmount = r.GetDecimal(4),
                            PaymentStatus = r.GetString(5),
                            PaymentMethod = r.IsDBNull(6) ? null : r.GetString(6),
                            BillingDate = r.GetDateTime(7)
                        });
                    }
                }
            }
            return list;
        }
    }
}

