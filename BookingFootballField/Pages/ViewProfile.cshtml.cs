using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages
{
    public class ViewProfileModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public ViewProfileModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            customerRepository = new CustomerRepository();
        }

        public BusinessObject.Model.Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToPage("/Index");
            }
            if (customerId != null)
            {
                name = customerRepository.GetName(Int32.Parse(customerId));
            }
            //Customer = customerRepository.GetByID(id);
            Customer = customerRepository.GetByID(Int32.Parse(customerId));

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

