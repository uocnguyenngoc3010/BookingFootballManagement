using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;

namespace BookingFootballField.Pages.BlackList
{
    public class DetailsModel : PageModel
    {
        public string name;
        IBlackListRepository blackListRepository = new BlackListRepository();
        IStaffRepository staffRepository = new StaffRepository();
        ICustomerRepository cusRepo = new CustomerRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public BusinessObject.Model.BlackList BlackList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            staffId = HttpContext.Session.GetString("StaffId");
            {
                if (staffId == null)
                {
                    return RedirectToPage("/Index");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            name = staffRepository.GetName(Int32.Parse(staffId));
            BlackList = blackListRepository.GetByID((int)id);
            BlackList.Customer = cusRepo.GetByID(BlackList.CustomerId);

            if (BlackList == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
