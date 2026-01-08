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
    public class PharmacistRepo
    {
        // CREATE
        public bool Add(Pharmacist pharmacist)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Pharmacists (UserID, LicenseNumber)
                  VALUES (@u, @l); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = pharmacist.UserID;
                cmd.Parameters.Add("@l", SqlDbType.NVarChar, 50).Value = pharmacist.LicenseNumber;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    pharmacist.PharmacistID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<Pharmacist> GetAll()
        {
            var list = new List<Pharmacist>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PharmacistID, UserID, LicenseNumber FROM Pharmacists", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Pharmacist
                        {
                            PharmacistID = r.GetInt32(0),
                            UserID = r.GetInt32(1),
                            LicenseNumber = r.GetString(2)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Pharmacist GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PharmacistID, UserID, LicenseNumber FROM Pharmacists WHERE PharmacistID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Pharmacist
                    {
                        PharmacistID = r.GetInt32(0),
                        UserID = r.GetInt32(1),
                        LicenseNumber = r.GetString(2)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Pharmacist pharmacist)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Pharmacists SET UserID = @u, LicenseNumber = @l 
                  WHERE PharmacistID = @id", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = pharmacist.UserID;
                cmd.Parameters.Add("@l", SqlDbType.NVarChar, 50).Value = pharmacist.LicenseNumber;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = pharmacist.PharmacistID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Pharmacists WHERE PharmacistID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
