using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LifeLink.Pages.IndexModel;

namespace LifeLink.Pages
{
    public class profileModel : PageModel
    {
        public ProfileInfo profileInfo = new ProfileInfo();
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() { 
            profileInfo.firstName = Request.Form["fname"];
            profileInfo.lastName = Request.Form["lname"];
            profileInfo.email = Request.Form["email"];
            profileInfo.phone = Request.Form["phone"];
            profileInfo.address = Request.Form["address"];

			if (profileInfo.firstName.Length == 0 ||
				profileInfo.lastName.Length == 0 ||
				profileInfo.email.Length == 0 ||
				profileInfo.phone.Length == 0 ||
				profileInfo.address.Length == 0 )
			{
				errorMessage = "All fields are required";
				return;
			}

            //save profile

            profileInfo.firstName = "";
            profileInfo.lastName = "";
            profileInfo.email = "";
            profileInfo.phone = "";
            profileInfo.address = "";
            successMessage = "Saved successfully";
		}
    }
}
