using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeLink.Pages
{
    public class SearchModel : PageModel
    {

        public string SearchTerm { get; set; }


		public void OnGet()
        {

        }
    }
}
