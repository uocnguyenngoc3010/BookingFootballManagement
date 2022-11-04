using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookingFootballField.Pages.BlackList
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository staffRepo = new StaffRepository();
        IBlackListRepository blackListRepository = new BlackListRepository();
        ICustomerRepository customerRepository = new CustomerRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public DeleteModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessObject.Model.BlackList BlackList { get; set; }
        [BindProperty]
        public BusinessObject.Model.Customer Customer { get; set; }
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
            Customer = customerRepository.GetByID(BlackList.CustomerId);

            if (BlackList == null)
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

            BlackList = blackListRepository.GetByID((int)id);
            Customer = customerRepository.GetByID(BlackList.CustomerId);
            Customer.Status = true;
            customerRepository.Update(Customer);
            blackListRepository.Update(BlackList);

            return RedirectToPage("./Index");
        }
    }
}
