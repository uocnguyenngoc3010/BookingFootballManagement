using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = null;
        IStaffRepository staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public DetailsModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            customerRepository = new CustomerRepository();
            staffRepository = new StaffRepository();
        }

        public BusinessObject.Model.Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (customerId != null)
            {
                name = customerRepository.GetName(Int32.Parse(customerId));
            }
            if (staffId != null)
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
            }
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            Customer = customerRepository.GetByID(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
