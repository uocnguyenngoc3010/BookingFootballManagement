using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using DataAccess.Repository;
using BusinessObject.Model;
using Microsoft.AspNetCore.Http;
using System;

namespace BookingFieldManagement.Pages.BookingField
{
    public class RequestHistoryModel : PageModel
    {        
        private IBookingRequestRepository reqRepo;
        private IBookingRecordRepository recordRepo;
        private ICustomerRepository cusRepo;
        public string name;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public RequestHistoryModel()
        {            
            reqRepo = new BookingRequestRepository();
            recordRepo = new BookingRecordRepository();
            cusRepo = new CustomerRepository();
        }

        [BindProperty]
        public int RequestId { get; set; }

        public List<BookingRequest> PendingRequests { get; set; }
        public List<BookingRequest> ResolvedRequests { get; set; }
        public List<BookingRequest> AllRequests { get; set; }
        public List<bool> CancelableApprovedRequests { get; set; }
        public IActionResult OnGet()
        {
            //Check if current user is Customer
            
            this.customerId = HttpContext.Session.GetString("CustomerId");

            //int customerId = 0;
            if (this.customerId != null)
            {
                var customerId = int.Parse(this.customerId);                
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("BookingField/RequestHistory: Hasn't Login Yet!");
                return RedirectToPage("/Index");
            }
            if (this.customerId != null)
            {
                name = cusRepo.GetName(Int32.Parse(this.customerId));
            }
            //Check for unactivated customer if unactivated return to Home Page
            var customer = cusRepo.GetByID(int.Parse(customerId));
            if (customer.Status == false)
            {
                return RedirectToPage("/Index");
            }

            PendingRequests = reqRepo.GetPendingRequestsByCustomerId(int.Parse(customerId));
            ResolvedRequests = reqRepo.GetResolvedRequestsByCustomerId(int.Parse(customerId));
            AllRequests = reqRepo.GetAllRequestsByCustomerId(int.Parse(customerId));
            CancelableApprovedRequests = new List<bool>();

            System.Diagnostics.Debug.WriteLine("Debug CancelableApprovedRequests:");
            foreach (var req in ResolvedRequests)
            {
                System.Diagnostics.Debug.WriteLine("Request ID " + req.Id + " - Cancelable Status: " + reqRepo.IsApprovedRequestCancelable(req.Id));
                CancelableApprovedRequests.Add(reqRepo.IsApprovedRequestCancelable(req.Id));
            }

            return Page();
        }

        // This is for customer to cancel pending request
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model Invalid! Page: BookingField/RequestHistory - Method: POST");
                return RedirectToPage("./RequestHistory");
            }

            // check if Session is still up
            int customerId = 0;

            if (HttpContext.Session.GetString("CustomerId") == null) return RedirectToPage("/Index");
            else
            {
                customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            }

            // check if booking request actually belongs to customer
            if (reqRepo.CheckValidRequestForCanceling(customerId, RequestId)) 
            {
                reqRepo.CustomerCancelsARequest(RequestId);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("You POS stop messing with the system!");
                return NotFound();
            }

            return RedirectToPage("./RequestHistory");
        }

        public IActionResult OnPostCancelApprovedRequest()
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model Invalid! Page: BookingField/RequestHistory - Method: POST - CancelApprovedRequest");
                return RedirectToPage("./RequestHistory");
            }

            // check if Session is still up
            int customerId = 0;

            if (HttpContext.Session.GetString("CustomerId") == null) return RedirectToPage("/Index");
            else
            {
                customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            }

            // check if the request is still available for canceling
            
            if (reqRepo.IsApprovedRequestCancelable(RequestId))
            {
                reqRepo.CancelApprovedRequest(RequestId);
                recordRepo.CancelApprovedRecord(RequestId);
            }

            return RedirectToPage("./RequestHistory");
        }
    }
}
