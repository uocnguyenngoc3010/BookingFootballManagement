using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace BookingFootballField.Pages.Staffs
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public EditModel(BusinessObject.Model.FBookingDBContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                _staffRepository.Update(staff);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!staffExists(staff.Id))
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

        private bool staffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
