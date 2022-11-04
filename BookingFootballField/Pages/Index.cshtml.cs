using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingFootballField.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        ICustomerRepository _customerRepository = new CustomerRepository();
        IStaffRepository _staffRepository = new StaffRepository();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            customerId = HttpContext.Session.GetString("CustomerId");
            staffId = HttpContext.Session.GetString("StaffId");
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (customerId != null)
            {
                name = _customerRepository.GetName(Int32.Parse(customerId));
            }
            if (staffId != null)
            {
                name = _staffRepository.GetName(Int32.Parse(staffId));
            }
            if (isAdmin != null)
            {
                name = "Admin";
            }
        }
    }
}
