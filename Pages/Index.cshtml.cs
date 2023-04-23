using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LifeLink.Pages
{
	public class IndexModel : PageModel
	{
		public List<UserInfo> listClients = new List<UserInfo>();
		public List<ProfileInfo> listProfiles = new List<ProfileInfo>();
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=DRKST-MTTR\\SQLEXPRESS;Initial Catalog=lifelink;Integrated Security=True";

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
			
			try
			{
				String connectionString = "Data Source=DRKST-MTTR\\SQLEXPRESS;Initial Catalog=lifelink;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM profileTable";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								ProfileInfo profileInfo = new ProfileInfo();
								profileInfo.profileId = "" + reader.GetInt32(0);
								profileInfo.firstName = reader.GetString(1);
								profileInfo.lastName = reader.GetString(2);
								profileInfo.email = reader.GetString(3);
								profileInfo.phone = reader.GetString(4);
								profileInfo.address = reader.GetString(5);
								profileInfo.bloodGroup = reader.GetString(5);
								profileInfo.RHfactor = reader.GetString(5);

								listProfiles.Add(profileInfo);
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
		
		public class ProfileInfo
		{
			public String profileId;
			public String firstName;
			public String lastName;
			public String email;
			public String phone;
			public String address;
			public String bloodGroup;
			public String RHfactor;
		}
	}
}