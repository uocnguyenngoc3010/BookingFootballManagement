using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookingFootballField.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = null;
        IStaffRepository staffRepository = null;
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

            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

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

            Customer = await _context.Customer.FindAsync(id);

            if (Customer != null)
            {
                customerRepository.Delete(Customer);
            }

            return RedirectToPage("./Index");
        }
    }
}
