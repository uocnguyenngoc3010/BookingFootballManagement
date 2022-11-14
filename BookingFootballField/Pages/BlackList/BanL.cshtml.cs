using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.BlackList
{
    public class BanLModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = new CustomerRepository();
        IBlackListRepository blackListRepository = new BlackListRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public BanLModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }

        public IList<BusinessObject.Model.Customer> Customer { get; set; }
        public IList<BusinessObject.Model.BlackList> BlackList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            staffId = HttpContext.Session.GetString("StaffId");
            if (staffId == null)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
                Customer = customerRepository.GetList();
                BlackList = blackListRepository.GetList();
                return Page();
            }
        }
    }
}
