using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace BookingFootballField.Pages.Staffs
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public DeleteModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _staffRepository = new StaffRepository();
        }

        [BindProperty]
        public BusinessObject.Model.Staff staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            staff = await _context.Staff.FirstOrDefaultAsync(m => m.Id == id);

            if (staff == null)
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

            staff = await _context.Staff.FindAsync(id);

            if (staff != null)
            {
                _staffRepository.Delete(staff);
            }

            return RedirectToPage("./Index");
        }
    }
}
