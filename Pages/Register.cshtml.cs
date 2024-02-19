using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using deliveryCompany.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace deliveryCompany.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly DeliveryCompanyContext _context;

        public RegisterModel(DeliveryCompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            _context.SaveChanges();

            return RedirectToPage("/Login");
        }
    }
}
