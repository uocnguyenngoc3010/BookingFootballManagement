using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace BookingFootballField.Pages.BookingField
{
    public class RecordHistoryModel : PageModel
    {
        private IBookingRecordRepository recordRepo;
        IStaffRepository staffRepository = new StaffRepository();
        ICustomerRepository customerRepository = new CustomerRepository();
        public string customerId;
        public string staffId;
        public string isAdmin;
        public string name;
        public RecordHistoryModel()
        {
            recordRepo = new BookingRecordRepository();
        }
        public List<BusinessObject.Model.BookingRecord> BookingRecords { get; set; }
        public IActionResult OnGet()
        {

            //Check if current user is a customer
            //int customerId = 0;

            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                this.customerId = HttpContext.Session.GetString("CustomerId");
                var customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            }
            if (this.customerId != null)
            {
                name = customerRepository.GetName(Int32.Parse(this.customerId));
            }
            else return RedirectToPage("/Index");
            BookingRecords = recordRepo.CustomerGetAllRecords(int.Parse(customerId));

            return Page();
        }
    }
}
