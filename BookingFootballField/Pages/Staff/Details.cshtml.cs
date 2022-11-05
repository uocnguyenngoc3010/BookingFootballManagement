using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
namespace BookingFootballField.Pages.Staffs
{
    public class DetailsModel : PageModel
    {
            private readonly BusinessObject.Model.FBookingDBContext _context;
            IStaffRepository _staffRepository = null;
            public string customerId;
            public string staffId;
            public string isAdmin;
            public string name;
            public DetailsModel(BusinessObject.Model.FBookingDBContext context)
            {
                _context = context;
                _staffRepository = new StaffRepository();
            }

            public BusinessObject.Model.staff staff { get; set; }

            public async Task<IActionResult> OnGetAsync(int id)
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

                staff = _staffRepository.GetByID(id);

                if (staff == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }
}
