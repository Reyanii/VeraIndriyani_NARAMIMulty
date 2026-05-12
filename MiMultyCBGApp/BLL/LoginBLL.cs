using System;
using System.Data;
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
            using (SqlConnection conn = DbConnection.GetConnection())
            {
             
                using (SqlCommand cmd = new SqlCommand("SP_LoginUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

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
