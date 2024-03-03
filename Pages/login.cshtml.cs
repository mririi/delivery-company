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
            var email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                // User is already logged in, redirect to index page
                return RedirectToPage("/Index");
            }

            // User is not logged in, show the login page
            return Page();
        }

        public IActionResult OnPost()
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Email == User.Email && u.Pass == User.Pass);
            if (userInDb != null)
            {
                HttpContext.Session.SetString("Email", User.Email);
                if (userInDb.UserType == 0)
                {
                    HttpContext.Session.SetString("UserType", "0");
                    return RedirectToPage("/Dashboard");
                }
                else
                {
                    HttpContext.Session.SetString("UserType", "1");
                    return RedirectToPage("/Index");
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return Page();
            }
        }
    }
}
