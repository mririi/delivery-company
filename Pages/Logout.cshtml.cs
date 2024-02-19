using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace deliveryCompany.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }
    }
}
