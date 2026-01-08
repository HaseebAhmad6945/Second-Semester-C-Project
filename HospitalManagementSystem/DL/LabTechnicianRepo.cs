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
    public class LabTechnicianRepo
    {
        // CREATE
        public bool Add(LabTechnician labTech)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO LabTechnicians (UserID, LabType)
                  VALUES (@u, @l); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = labTech.UserID;
                cmd.Parameters.Add("@l", SqlDbType.NVarChar, 100).Value = labTech.LabType;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    labTech.LabTechnicianID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<LabTechnician> GetAll()
        {
            var list = new List<LabTechnician>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT LabTechnicianID, UserID, LabType FROM LabTechnicians", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new LabTechnician
                        {
                            LabTechnicianID = r.GetInt32(0),
                            UserID = r.GetInt32(1),
                            LabType = r.GetString(2)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public LabTechnician GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT LabTechnicianID, UserID, LabType FROM LabTechnicians WHERE LabTechnicianID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new LabTechnician
                    {
                        LabTechnicianID = r.GetInt32(0),
                        UserID = r.GetInt32(1),
                        LabType = r.GetString(2)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(LabTechnician labTech)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE LabTechnicians SET UserID = @u, LabType = @l 
                  WHERE LabTechnicianID = @id", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.Int).Value = labTech.UserID;
                cmd.Parameters.Add("@l", SqlDbType.NVarChar, 100).Value = labTech.LabType;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = labTech.LabTechnicianID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM LabTechnicians WHERE LabTechnicianID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

