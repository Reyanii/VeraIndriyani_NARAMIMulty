using System;
using System.Data.SqlClient;
using MiMultyCBGApp.DAL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp.BLL
{
    public class LoginBLL
    {
        public User AuthenticateUser(string username, string password)
        {
            User user = null;
            string hashedPassword = SecurityHelper.HashPassword(password);
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT UserID, Username, Role, ID_Cabang FROM Users WHERE Username=@Username AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Role = reader["Role"].ToString().Trim(),
                                    ID_Cabang = reader["ID_Cabang"] != DBNull.Value ? Convert.ToInt32(reader["ID_Cabang"]) : 0
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error saat login: " + ex.Message);
                    }
                }
            }
            return user;
        }
    }
}
