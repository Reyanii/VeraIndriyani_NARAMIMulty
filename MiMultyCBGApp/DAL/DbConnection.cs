using System.Configuration;
using System.Data.SqlClient;

namespace MiMultyCBGApp.DAL
{
    public class DbConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
