using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
namespace BookingFootballField.Pages.Staffs
{
    public class ViewProfileModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IStaffRepository _staffRepository = null;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public ViewProfileModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
            _staffRepository = new StaffRepository();
        }

        public BusinessObject.Model.Staff staff { get; set; }

        public IActionResult OnGet()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now + " I'm now here");
            staffId = HttpContext.Session.GetString("StaffId");
            if (staffId != null)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + " I'm here");
                name = _staffRepository.GetName(Int32.Parse(staffId));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + " Why the hell staffId is null fuckkkkkkkkk");
                return RedirectToPage("/Index");
            }

            staff = _staffRepository.GetByID(int.Parse(staffId));

            if (staff == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
