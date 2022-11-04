using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository _customerRepository = null;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public IndexModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _customerRepository = new CustomerRepository();
            _staffRepository = new StaffRepository();
        }

        public IList<BusinessObject.Model.Customer> Customer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            //Customer = await _context.Customers.ToListAsync();
            if (staffId != null)
            {
                name = _staffRepository.GetName(Int32.Parse(staffId));
            }
            if (customerId != null)
            {
                name = _customerRepository.GetName(Int32.Parse(customerId));
            }
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            Customer = _customerRepository.GetList();
            return Page();

        }
    }
}
