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
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository=null;
        IStaffRepository staffRepository=null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public DeleteModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            customerRepository = new CustomerRepository();
            staffRepository = new StaffRepository();
        }

        [BindProperty]
        public BusinessObject.Model.Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
          
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FindAsync(id);

            if (Customer != null)
            {
                customerRepository.Delete(Customer);
            }

            return RedirectToPage("./Index");
        }
    }
}
