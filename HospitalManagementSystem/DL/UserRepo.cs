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
    public class UserRepo
    {
        // CREATE - Add new user (Sign Up)
        public bool AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Users (Email, PasswordHash, Role, CreatedAt)
                  VALUES (@e, @p, @r, @c); 
                  SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.Add("@e", SqlDbType.NVarChar, 255).Value = user.Email;
                cmd.Parameters.Add("@p", SqlDbType.NVarChar, 255).Value = user.PasswordHash;
                cmd.Parameters.Add("@r", SqlDbType.NVarChar, 50).Value = user.Role;
                cmd.Parameters.Add("@c", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                try
                {
                    object result = cmd.ExecuteScalar();
                    user.UserID = Convert.ToInt32(result);
                    return true;
                }
                catch (SqlException ex)
                {
                    // 2627, 2601 = unique constraint violation (duplicate email)
                    if (ex.Number == 2627 || ex.Number == 2601)
                        return false;
                    throw;
                }
            }
        }

        // READ - Sign In (Authentication)
        public User SignIn(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT UserID, Email, PasswordHash, Role, CreatedAt FROM Users WHERE Email = @e", conn))
            {
                cmd.Parameters.Add("@e", SqlDbType.NVarChar, 255).Value = email;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    string dbPass = reader.GetString(reader.GetOrdinal("PasswordHash"));
                    if (dbPass != password)
                        return null;

                    return new User
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PasswordHash = dbPass,
                        Role = reader.GetString(reader.GetOrdinal("Role")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                    };
                }
            }
        }

        // READ - Get all users
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT UserID, Email, PasswordHash, Role, CreatedAt FROM Users", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        users.Add(new User
                        {
                            UserID = r.GetInt32(0),
                            Email = r.GetString(1),
                            PasswordHash = r.GetString(2),
                            Role = r.GetString(3),
                            CreatedAt = r.GetDateTime(4)
                        });
                    }
                }
            }
            return users;
        }

        // READ - Get user by ID
        public User GetById(int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT UserID, Email, PasswordHash, Role, CreatedAt FROM Users WHERE UserID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    return new User
                    {
                        UserID = r.GetInt32(0),
                        Email = r.GetString(1),
                        PasswordHash = r.GetString(2),
                        Role = r.GetString(3),
                        CreatedAt = r.GetDateTime(4)
                    };
                }
            }
        }

        // UPDATE - Update user details
        public bool Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE Users 
                  SET Email = @e, PasswordHash = @p, Role = @r 
                  WHERE UserID = @id", conn))
            {
                cmd.Parameters.Add("@e", SqlDbType.NVarChar, 255).Value = user.Email;
                cmd.Parameters.Add("@p", SqlDbType.NVarChar, 255).Value = user.PasswordHash;
                cmd.Parameters.Add("@r", SqlDbType.NVarChar, 50).Value = user.Role;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.UserID;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // DELETE - Delete user by ID
        public bool Delete(int userId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Users WHERE UserID = @id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // READ - Get users by role
        public List<User> GetByRole(string role)
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT UserID, Email, PasswordHash, Role, CreatedAt FROM Users WHERE Role = @r", conn))
            {
                cmd.Parameters.Add("@r", SqlDbType.NVarChar, 50).Value = role;
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        users.Add(new User
                        {
                            UserID = r.GetInt32(0),
                            Email = r.GetString(1),
                            PasswordHash = r.GetString(2),
                            Role = r.GetString(3),
                            CreatedAt = r.GetDateTime(4)
                        });
                    }
                }
            }
            return users;
        }
    }
}

