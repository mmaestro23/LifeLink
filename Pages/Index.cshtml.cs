using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LifeLink.Pages
{
	public class IndexModel : PageModel
	{
		public List<UserInfo> listClients = new List<UserInfo>();
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=lifeLink;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM Users";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								UserInfo userInfo = new UserInfo();
								userInfo.id = "" + reader.GetInt32(0);
								userInfo.firstName = reader.GetString(1);
								userInfo.lastName = reader.GetString(2);
								userInfo.email = reader.GetString(3);
								userInfo.phone = reader.GetString(4);
								userInfo.address = reader.GetString(5);
								userInfo.password = reader.GetString(6);

								listClients.Add(userInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
		public class UserInfo
		{
			public String id;
			public String firstName;
			public String lastName;
			public String email;
			public String phone;
			public String address;
			public String password;
		}
	}
}