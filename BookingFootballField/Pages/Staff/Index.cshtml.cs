using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookingFootballField.Pages.Staffs
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public IndexModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _staffRepository = new StaffRepository();
        }

        public IList<BusinessObject.Model.staff> staff { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            staff = _staffRepository.GetList();
            return Page();
        }
    }
}
