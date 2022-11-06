using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.BlackList
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;

        IBlackListRepository blackListRepository = new BlackListRepository();
        ICustomerRepository customerRepository = new CustomerRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;

        public int CustomerId { get; set; }
        public BusinessObject.Model.Customer Customer { get; set; }


        [BindProperty]
        public BusinessObject.Model.BlackList BlackList { get; set; }

        public CreateModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            staffId = HttpContext.Session.GetString("StaffId");
            if (staffId != null)
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
            }
            {
                if (staffId == null)
                {
                    return RedirectToPage("/Index");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            CustomerId = (int)id;

            Customer = customerRepository.GetByID((int)id);

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Customer = customerRepository.GetByID((int)id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            BlackList.CustomerId = Customer.Id;
            //BlackList.Customer = Customer;
            BlackList.BanDate = DateTime.Now;
            Customer.Status = false;
            customerRepository.Update(Customer);
            blackListRepository.Add(BlackList);
            return RedirectToPage("./Index");
        }
    }
}
