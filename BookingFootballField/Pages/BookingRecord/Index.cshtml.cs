using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BookingFootballField.Pages.BookingRecord
{
    public class IndexModel : PageModel
    {
        private IBookingRecordRepository recordRepo;
        IStaffRepository staffRepo;
        public string name;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public IndexModel()
        {
            recordRepo = new BookingRecordRepository();
            staffRepo = new StaffRepository();
        }

        public List<BusinessObject.Model.BookingRecord> BookingRecords { get; set; }
        public IActionResult OnGet()
        {
            //Check if current user is Staff or Admin                        
            bool isStaff = false;
            int staffId = 0;

            if (HttpContext.Session.GetString("StaffId") != null)
            {
                this.staffId = HttpContext.Session.GetString("StaffId");
                isStaff = true;
                staffId = int.Parse(HttpContext.Session.GetString("StaffId"));
            }

            if (isStaff)
            {
                var staff = staffRepo.GetByID(staffId);
                name = staff.Name;
                BookingRecords = recordRepo.GetAllRecords();
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
