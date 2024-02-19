using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using deliveryCompany.Models;
using System.Linq;

namespace deliveryCompany.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DeliveryCompanyContext _context;

        public LoginModel(DeliveryCompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userName))
            {
                // User is already logged in, redirect to index page
                return RedirectToPage("/Index");
            }

            // User is not logged in, show the login page
            return Page();
        }

        public IActionResult OnPost()
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.UserName == User.UserName && u.Pass == User.Pass);
            if (userInDb != null)
            {
                HttpContext.Session.SetString("UserName", User.UserName);
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return Page();
            }
        }
    }
}
