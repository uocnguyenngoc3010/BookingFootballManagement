using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace BookingFootballField.Pages.BookingField
{
    public class IndexModel : PageModel
    {
        IStaffRepository staffRepository = new StaffRepository();
        ICustomerRepository customerRepository = new CustomerRepository();
        private IFootballFieldRepository fbRepo;
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public IndexModel()
        {
            fbRepo = new FootballFieldRepository();
        }
        public List<BusinessObject.Model.FootballField> FootballFields { get; set; }
        public IActionResult OnGet()
        {

            var customer = HttpContext.Session.GetString("CustomerId");
            if (customer != null)
            {
                this.customerId = customer;
                FootballFields = fbRepo.GetAllAvailableFields();
            }
            else
            {
                return RedirectToPage("/Index");
            }
            if (this.customerId != null)
            {
                name = customerRepository.GetName(Int32.Parse(this.customerId));
            }


            return Page();
        }
    }
}
