using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace BookingFieldManagement.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository _customerRepository=null;      
        IStaffRepository _staffRepository=null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public CreateModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _customerRepository = new CustomerRepository();
            _staffRepository = new StaffRepository();
        }

        public IActionResult OnGet()
        {
            if (staffId != null)
            {
                name = _staffRepository.GetName(Int32.Parse(staffId));
            }
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
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
