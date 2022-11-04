using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BookingFootballField.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = null;
        IStaffRepository staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;

        public EditModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            customerRepository = new CustomerRepository();
            staffRepository = new StaffRepository();
        }

        [BindProperty]
        public BusinessObject.Model.Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            isAdmin = HttpContext.Session.GetString("isAdmin");
            customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId != null)
            {
                name = customerRepository.GetName(Int32.Parse(customerId));
            }
            if (isAdmin == null && customerId == null)
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
            else
            {
                if (customerId != null && Customer.Id != int.Parse(customerId))
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                customerRepository.Update(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (HttpContext.Session.GetString("isAdmin") != null) return RedirectToPage("./Index");
            // is a customer
            return RedirectToPage("/ViewProfile");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
