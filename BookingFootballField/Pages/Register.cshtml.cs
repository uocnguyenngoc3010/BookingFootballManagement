using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookingFootballField.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository _customerRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public RegisterModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _customerRepository = new CustomerRepository();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObject.Model.Customer Customer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_customerRepository.GetByEmail(Customer.Email) != null)
            {
                ModelState.AddModelError("Customer.Email", "Email is already exist!");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer.Status = true;
            _customerRepository.Add(Customer);

            return RedirectToPage("./Index");
        }
    }
}
