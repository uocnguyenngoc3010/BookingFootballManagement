using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using static BusinessObject.Model.Staff;
namespace BookingFootballField.Pages.Staffs
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public CreateModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _staffRepository = new StaffRepository();
        }

        public IActionResult OnGet()
        {
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public BusinessObject.Model.Staff staff { get; set; }
        //name space trung voi ten class k goi duoc staff ra
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_staffRepository.GetByEmail(staff.Email) != null)
            {
                ModelState.AddModelError("staff.Email", "Email is already exist!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _staffRepository.Add(staff);

            return RedirectToPage("./Index");
        }
    }
}
