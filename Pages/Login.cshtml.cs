using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LifeLink.Pages.IndexModel;

namespace LifeLink.Pages
{
    public class LoginModel : PageModel
    {
		public UserInfo userInfo = new UserInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
		}

		public void OnPost()
		{
			userInfo.email = Request.Form["email"];
			userInfo.password = Request.Form["password"];

			if (userInfo.email.Length == 0 || userInfo.password.Length == 0)
			{
				errorMessage = "All fields are required MR/MRS";
				return;
			}

			try
			{
				String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=lifeLink;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@email", userInfo.email);
						command.Parameters.AddWithValue("@password", userInfo.password);

						int count = (int)command.ExecuteScalar();
						if (count == 0)
						{
							errorMessage = "Invalid email or password.";
							return;
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			userInfo.email = "";
			userInfo.password = "";

			successMessage = "Logged in Successfully";

			Response.Redirect("/Index");
		}
	}
}
