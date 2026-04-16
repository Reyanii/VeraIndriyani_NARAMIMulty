using System.Data.SqlClient;

namespace MiMultyCBGApp.DAL
{
    public class DbConnection
    {

        private static string connectionString = @"Server=NALZ\MUHAMMADBAGAS;Database=MIMultiCBGDB;User ID=sa;Password=Ragehaste90;Connection Timeout=5;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
