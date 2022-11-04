using BusinessObject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingFootballField.Pages
{
    public class LoginModel : PageModel
    {
        public Authenticate authen = new Authenticate();
        private readonly FBookingDBContext _context;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public LoginModel(FBookingDBContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string email, string password)
        {
            if (authen.IsAdmin(email, password))
            {
                HttpContext.Session.SetString("isAdmin", "true");
                return RedirectToPage("/Index");
            }
            else if (authen.LoginByStaff(email, password) != null)
            {
                var staff = authen.LoginByStaff(email, password);
                HttpContext.Session.SetString("StaffId", staff.Id.ToString());
                return RedirectToPage("/Index");
            }
            else if (authen.LoginByCustomer(email, password) != null)
            {
                BusinessObject.Model.Customer customer = authen.LoginByCustomer(email, password);
                HttpContext.Session.SetString("CustomerId", customer.Id.ToString());
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["errmsg"] = "Wrong email or password";
                return Page();
            }
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("isAdmin");
            HttpContext.Session.Remove("StaffId");
            HttpContext.Session.Remove("CustomerId");
            return Page();
        }
    }
}
