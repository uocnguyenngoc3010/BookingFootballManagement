using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using BusinessObject.Model;
using DataAccess.Repository;
using DataAccess.Helper;
using System;

namespace BookingFieldManagement.Pages.BookingField
{
    public class ResolveRequestsModel : PageModel
    {
        private IBookingRequestRepository reqRepo;
        private IBookingRecordRepository recRepo;
        private IStaffRepository staffRepo;
        private BookingSlotHelper slotHelper;
        public string name;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public ResolveRequestsModel()
        {
            reqRepo = new BookingRequestRepository();
            recRepo = new BookingRecordRepository();
            staffRepo = new StaffRepository();
            slotHelper = new BookingSlotHelper();
        }

        [BindProperty]
        public int RequestId { get; set; }
        [BindProperty]
        public string ActionName { get; set; }

        public List<BookingRequest> PendingRequests = new List<BookingRequest>();
        public List<BookingRequest> AllRequests = new List<BookingRequest>();
        private string ACTION_APPROVE = "APPROVE";
        private string ACTION_DECLINE = "DECLINE";

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
                var currStaff = staffRepo.GetByID(staffId);

                if (currStaff.IsActived != null && currStaff.IsActived != false)
                {
                    name = currStaff.Name;

                    PendingRequests = reqRepo.GetPendingRequestsOfCustomersForResolving(slotHelper.NumOfDaysAvailableForBooking);

                    AllRequests = reqRepo.GetAllRequests();
                }
                
                // Unactivated staff returns back to home page
                else return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model Invalid! Page: BookingField/ResolveRequests - Method: POST");
                return RedirectToPage(NotFound());
            }
            else
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
                else return RedirectToPage("/Index");

                try
                {
                    if (isStaff)
                    {
                        if (ActionName.Equals(ACTION_APPROVE))
                        {
                            var currReq = reqRepo.GetRequestById(RequestId);

                            recRepo.AddARecord(new BusinessObject.Model.BookingRecord()
                            {
                                BookingRequestId = RequestId,
                                StaffId = staffId,
                                Status = ""
                            });
                            reqRepo.ApproveARequest(RequestId);
                        }
                        else if (ActionName.Equals(ACTION_DECLINE))
                        {
                            reqRepo.DeclineARequest(RequestId);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("BookingField/ResolveRequests: How the hell u get here?");
                            return RedirectToPage(NotFound());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            return RedirectToPage("./ResolveRequests");
        }
    }
}
