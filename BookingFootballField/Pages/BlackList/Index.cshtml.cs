using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.BlackList
{
    public class IndexModel : PageModel
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        IBlackListRepository blackListRepository = new BlackListRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public IList<BusinessObject.Model.BlackList> BlackList { get; set; }
        public IList<BusinessObject.Model.Customer> Customer { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            staffId = HttpContext.Session.GetString("StaffId");
            {
                if (staffId == null)
                {
                    return RedirectToPage("/Index");
                }
            }
            if (staffId != null)
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
            }
            BlackList = blackListRepository.GetList();
            Customer = customerRepository.GetList();
            return Page();
        }
    }
}
