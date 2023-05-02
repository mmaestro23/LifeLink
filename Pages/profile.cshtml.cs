using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static LifeLink.Pages.IndexModel;

namespace LifeLink.Pages
{
    public class profileModel : PageModel
    {
        public ProfileInfo profileInfo = new ProfileInfo();
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
		
		public List<string> bloodGroups { get; set; } = new List<string> { "A", "B", "AB", "O" };
		public void OnGet()
		{
			/*if (LoginModel.userId == null)
			{
				errorMessage = "User not logged in.";
				return;
			}

			try
			{
				String connectionString = "Data Source=DRKST-MTTR\\SQLEXPRESS;Initial Catalog=lifelink;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM profileTable WHERE userId = @profileId";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@profileId", LoginModel.userId);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								userInfo.id = "" + reader.GetInt32(0);
								userInfo.firstName = reader.GetString(1);
								userInfo.lastName = reader.GetString(2);
								userInfo.email = reader.GetString(3);
								userInfo.phone = reader.GetString(4);
								userInfo.address = reader.GetString(5);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}*/
		}


		public void OnPost() { 
            profileInfo.firstName = Request.Form["fname"];
            profileInfo.lastName = Request.Form["lname"];
            profileInfo.email = Request.Form["email"];
            profileInfo.phone = Request.Form["phone"];
            profileInfo.address = Request.Form["address"];
            profileInfo.bloodGroup = Request.Form["bloodgroup"];
            profileInfo.RHfactor = Request.Form["rhfactor"];

			if (profileInfo.firstName.Length == 0 || profileInfo.lastName.Length == 0 || profileInfo.email.Length == 0 ||
				profileInfo.phone.Length == 0 || profileInfo.address.Length == 0 || profileInfo.bloodGroup.Length == 0 ||
				profileInfo.RHfactor.Length == 0)
			{
				errorMessage = "All fields are required";
				return;
			}

            //save profile

            try
            {
				String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=lifeLink;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
					String sql = "INSERT INTO profileTable " +
								 "(firstName, lastName, email, phone, address, bloodGroup, RHfactor) VALUES " +
								 "(@fname, @lname, @email, @phone, @address, @bloodgroup, @rhfactor);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@fname", profileInfo.firstName);
						command.Parameters.AddWithValue("@lname", profileInfo.lastName);
						command.Parameters.AddWithValue("@email", profileInfo.email);
						command.Parameters.AddWithValue("@phone", profileInfo.phone);
						command.Parameters.AddWithValue("@address", profileInfo.address);
						command.Parameters.AddWithValue("@bloodgroup", profileInfo.bloodGroup);
						command.Parameters.AddWithValue("@rhfactor", profileInfo.RHfactor);

						command.ExecuteNonQuery();
					}
				}
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            profileInfo.firstName = "";
            profileInfo.lastName = "";
            profileInfo.email = "";
            profileInfo.phone = "";
            profileInfo.address = "";
            profileInfo.bloodGroup = "";
            profileInfo.RHfactor = "";
            successMessage = "Saved successfully";
		}
    }
}
