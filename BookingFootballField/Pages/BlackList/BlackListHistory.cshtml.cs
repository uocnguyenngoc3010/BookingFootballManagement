using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingFootballField.Pages.BlackList
{
    public class BlackListHistoryModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        ICustomerRepository customerRepository = new CustomerRepository();
        IStaffRepository staffRepo = new StaffRepository();
        IBlackListRepository blackListRepository = new BlackListRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public BlackListHistoryModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }

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

            name = staffRepo.GetByID(int.Parse(staffId)).Name;
            BlackList = blackListRepository.GetList();
            Customer = customerRepository.GetList();
            return Page();
        }
    }
}
