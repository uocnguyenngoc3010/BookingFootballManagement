using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace BookingFieldManagement.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository _customerRepository=null;
        IStaffRepository _staffRepository=null;
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

        public IList<BusinessObject.Model.Customer> Customer { get;set; }

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
