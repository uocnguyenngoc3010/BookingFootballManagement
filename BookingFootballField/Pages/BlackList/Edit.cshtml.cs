using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookingFootballField.Pages.BlackList
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;

        IBlackListRepository blackListRepository = new BlackListRepository();
        IStaffRepository staffRepo = new StaffRepository();
        ICustomerRepository customerRepository = new CustomerRepository();
        public string staffId;
        public string customerId;
        public string isAdmin;
        public string name;
        public EditModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessObject.Model.BlackList BlackList { get; set; }
        ////public BusinessObject.Model.Customer Customer { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            staffId = HttpContext.Session.GetString("StaffId");
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

            name = staffRepo.GetName(int.Parse(staffId));
            BlackList = blackListRepository.GetByID((int)id);
            if (BlackList == null)
            {
                return NotFound();
            }
            //Customer = customerRepository.GetByID(BlackList.CustomerId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(BlackList).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                blackListRepository.Update(BlackList);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlackListExists(BlackList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlackListExists(int id)
        {
            //return _context.BlackLists.Any(e => e.Id == id);
            BusinessObject.Model.BlackList blackList = blackListRepository.GetByID(id);
            if (blackList == null)
                return false;
            else
                return true;
        }
    }
}
