using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace BookingFieldManagement.Pages.FootballField
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IFootballFieldRepository footballFieldRepository = new FootballFieldRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string name;
        public DetailsModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }
        public string staffId;
        public string isAdmin;
        public string customerId;
        public BusinessObject.Model.FootballField FootballField { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (staffId != null)
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
            }
            customerId = HttpContext.Session.GetString("CustomerId");
            staffId = HttpContext.Session.GetString("StaffId");
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (staffId == null && isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            FootballField = footballFieldRepository.GetByID((int)id);

            if (FootballField == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
