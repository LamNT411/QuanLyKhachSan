using Microsoft.Data.SqlClient;

namespace QuanLyKhachSan.Database
{
	public class DB
	{
		private static string connectionString =
		@"Server=.;
          Database=QLKhachSan;
          Trusted_Connection=True;
          TrustServerCertificate=True";

		public static SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}
	}
}