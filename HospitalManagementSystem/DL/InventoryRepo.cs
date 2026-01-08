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
    public class InventoryRepo
    {
        // CREATE
        public bool Add(Inventory inventory)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Inventory (MedicineName, Category, Manufacturer, UnitPrice, Quantity, ExpiryDate, ReorderLevel, LastRestocked, CreatedAt)
                  VALUES (@m, @c, @mf, @up, @q, @ed, @rl, @lr, @ca); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@m", SqlDbType.NVarChar, 200).Value = inventory.MedicineName;
                cmd.Parameters.Add("@c", SqlDbType.NVarChar, 100).Value =
                    (object)inventory.Category ?? DBNull.Value;
                cmd.Parameters.Add("@mf", SqlDbType.NVarChar, 200).Value =
                    (object)inventory.Manufacturer ?? DBNull.Value;
                cmd.Parameters.Add("@up", SqlDbType.Decimal).Value = inventory.UnitPrice;
                cmd.Parameters.Add("@q", SqlDbType.Int).Value = inventory.Quantity;
                cmd.Parameters.Add("@ed", SqlDbType.Date).Value =
                    (object)inventory.ExpiryDate ?? DBNull.Value;
                cmd.Parameters.Add("@rl", SqlDbType.Int).Value = inventory.ReorderLevel;
                cmd.Parameters.Add("@lr", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@ca", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    inventory.InventoryID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // READ - Get all
        public List<Inventory> GetAll()
        {
            var list = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT InventoryID, MedicineName, Category, Manufacturer, UnitPrice, Quantity, ExpiryDate, ReorderLevel, LastRestocked, CreatedAt FROM Inventory", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Inventory
                        {
                            InventoryID = r.GetInt32(0),
                            MedicineName = r.GetString(1),
                            Category = r.IsDBNull(2) ? null : r.GetString(2),
                            Manufacturer = r.IsDBNull(3) ? null : r.GetString(3),
                            UnitPrice = r.GetDecimal(4),
                            Quantity = r.GetInt32(5),
                            ExpiryDate = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                            ReorderLevel = r.GetInt32(7),
                            LastRestocked = r.GetDateTime(8),
                            CreatedAt = r.GetDateTime(9)
                        });
                    }
                }
            }
            return list;
        }

        // READ - Get by ID
        public Inventory GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT InventoryID, MedicineName, Category, Manufacturer, UnitPrice, Quantity, ExpiryDate, ReorderLevel, LastRestocked, CreatedAt FROM Inventory WHERE InventoryID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Inventory
                    {
                        InventoryID = r.GetInt32(0),
                        MedicineName = r.GetString(1),
                        Category = r.IsDBNull(2) ? null : r.GetString(2),
                        Manufacturer = r.IsDBNull(3) ? null : r.GetString(3),
                        UnitPrice = r.GetDecimal(4),
                        Quantity = r.GetInt32(5),
                        ExpiryDate = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                        ReorderLevel = r.GetInt32(7),
                        LastRestocked = r.GetDateTime(8),
                        CreatedAt = r.GetDateTime(9)
                    };
                }
            }
        }

        // UPDATE
        public bool Update(Inventory inventory)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Inventory 
                  SET MedicineName = @m, Category = @c, Manufacturer = @mf, UnitPrice = @up, 
                      Quantity = @q, ExpiryDate = @ed, ReorderLevel = @rl, LastRestocked = @lr 
                  WHERE InventoryID = @id", conn))
            {
                cmd.Parameters.Add("@m", SqlDbType.NVarChar, 200).Value = inventory.MedicineName;
                cmd.Parameters.Add("@c", SqlDbType.NVarChar, 100).Value =
                    (object)inventory.Category ?? DBNull.Value;
                cmd.Parameters.Add("@mf", SqlDbType.NVarChar, 200).Value =
                    (object)inventory.Manufacturer ?? DBNull.Value;
                cmd.Parameters.Add("@up", SqlDbType.Decimal).Value = inventory.UnitPrice;
                cmd.Parameters.Add("@q", SqlDbType.Int).Value = inventory.Quantity;
                cmd.Parameters.Add("@ed", SqlDbType.Date).Value =
                    (object)inventory.ExpiryDate ?? DBNull.Value;
                cmd.Parameters.Add("@rl", SqlDbType.Int).Value = inventory.ReorderLevel;
                cmd.Parameters.Add("@lr", SqlDbType.DateTime).Value = inventory.LastRestocked;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = inventory.InventoryID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Inventory WHERE InventoryID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Get items needing reorder
        public List<Inventory> GetItemsNeedingReorder()
        {
            var list = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT InventoryID, MedicineName, Category, Manufacturer, UnitPrice, Quantity, ExpiryDate, ReorderLevel, LastRestocked, CreatedAt FROM Inventory WHERE Quantity <= ReorderLevel", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Inventory
                        {
                            InventoryID = r.GetInt32(0),
                            MedicineName = r.GetString(1),
                            Category = r.IsDBNull(2) ? null : r.GetString(2),
                            Manufacturer = r.IsDBNull(3) ? null : r.GetString(3),
                            UnitPrice = r.GetDecimal(4),
                            Quantity = r.GetInt32(5),
                            ExpiryDate = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                            ReorderLevel = r.GetInt32(7),
                            LastRestocked = r.GetDateTime(8),
                            CreatedAt = r.GetDateTime(9)
                        });
                    }
                }
            }
            return list;
        }
    }
}
