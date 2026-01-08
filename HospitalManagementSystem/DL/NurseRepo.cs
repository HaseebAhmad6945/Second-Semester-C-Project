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
    public class NurseRepo
    {
        // CREATE
        public bool Add(Nurse nurse)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Nurses (UserID, Department, Shift)
                  VALUES (@u, @d, @s); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = nurse.UserID;
                cmd.Parameters.Add("@d", SqlDbType.NVarChar, 100).Value = nurse.Department;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 50).Value =
                    (object)nurse.Shift ?? DBNull.Value;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    nurse.NurseID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<Nurse> GetAll()
        {
            var list = new List<Nurse>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT NurseID, UserID, Department, Shift FROM Nurses", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Nurse
                        {
                            NurseID = r.GetInt32(0),
                            UserID = r.GetInt32(1),
                            Department = r.GetString(2),
                            Shift = r.IsDBNull(3) ? null : r.GetString(3)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Nurse GetById(int nurseId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT NurseID, UserID, Department, Shift FROM Nurses WHERE NurseID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = nurseId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Nurse
                    {
                        NurseID = r.GetInt32(0),
                        UserID = r.GetInt32(1),
                        Department = r.GetString(2),
                        Shift = r.IsDBNull(3) ? null : r.GetString(3)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Nurse nurse)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Nurses SET UserID = @u, Department = @d, Shift = @s 
                  WHERE NurseID = @id", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = nurse.UserID;
                cmd.Parameters.Add("@d", SqlDbType.NVarChar, 100).Value = nurse.Department;
                cmd.Parameters.Add("@s", SqlDbType.NVarChar, 50).Value =
                    (object)nurse.Shift ?? DBNull.Value;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = nurse.NurseID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int nurseId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Nurses WHERE NurseID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = nurseId;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

