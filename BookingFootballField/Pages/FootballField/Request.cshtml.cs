using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Model;
using DataAccess.Repository;
using DataAccess.Helper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;


namespace BookingFieldManagement.Pages.BookingField
{
    public class RequestModel : PageModel
    {        
        private IBookingRequestRepository reqRepo;
        private IBookingRecordRepository recordRepo;
        private IFootballFieldRepository fieldRepo;
        private ICustomerRepository cusRepo;
        private BookingSlotHelper slotHelper;
        public string name;
        public string customerId;
        public string staffId;
        public string isAdmin;

        public RequestModel()
        {            
            reqRepo = new BookingRequestRepository();
            recordRepo = new BookingRecordRepository();
            cusRepo = new CustomerRepository();
            fieldRepo = new FootballFieldRepository();
            slotHelper = new BookingSlotHelper();            

            NumOfSlots = slotHelper.NumOfDaysAvailableForBooking * slotHelper.NumOfSlotPerDay;
            ShownWeeks = slotHelper.NumOfDaysAvailableForBooking / 7;
            SlotsPerDay = slotHelper.NumOfSlotPerDay;
            TimeSlots = slotHelper.TimeSlots;
            CurrDate = System.DateTime.Now;
        }

        [BindProperty]
        public List<int> ChosenSlots { get; set; }
        [BindProperty]
        public int CurrFieldId { get; set; }

        public BusinessObject.Model.FootballField CurrField { get; set; }
        public DateTime CurrDate { get; set; }
        public int NumOfSlots { get; set; }
        public int ShownWeeks { get; set; }
        public int SlotsPerDay { get; set; }
        public Dictionary<int, string> TimeSlots { get; set; }

        // Slots of the current day that has passed the time for booking
        public List<int> UnavailableSlots { get; set; }
        public List<int> ApprovedSlots { get; set; }
        public List<int> CurrCustomerBookedSlots { get; set; }
        public List<int> CurrCustomerPendingSlots { get; set; }

        public IActionResult OnGet(int? fieldId)
        {
            
            if (fieldId == null)
            {
                return NotFound();
            }

            CurrField = fieldRepo.GetByID((int)fieldId);

            //Get the current customer's booked slots & requested slots (Needs CustomerId From Session)

            //Check if current user is admin
            bool isAdmin = false;
            if (HttpContext.Session.GetString("isAdmin") != null)
            {
                isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin"));
            }

            if (isAdmin) return RedirectToPage("/Index");

            else
            {
                var customerId = HttpContext.Session.GetString("CustomerId");
                if (customerId != null)
                {
                    name = cusRepo.GetName(Int32.Parse(customerId));
                }
                if (customerId != null)
                {
                    this.customerId = customerId;

                    try
                    {
                        //System.Diagnostics.Debug.WriteLine("Booking Slots: " + slotHelper.BookingSlots.Count);
                        int slotCount = 0;
                        foreach(var slot in slotHelper.BookingSlots)
                        {
                            //System.Diagnostics.Debug.WriteLine("Slot " + slotCount + ": " + slot);
                            slotCount++;
                        }

                        int customerIdAsInt = int.Parse(customerId);

                        var customer = cusRepo.GetByID(customerIdAsInt);
                        // Customer is unactivated return back to home page
                        if (customer.Status == false) return RedirectToPage("/Index");

                        CurrCustomerBookedSlots = slotHelper.getListOfUnavailableSlotsInInt(recordRepo.GetRecordsOfCustomerForBooking(customerIdAsInt, (int) fieldId));

                        CurrCustomerPendingSlots = slotHelper.getListOfUnavailableSlotsInInt(reqRepo.GetPendingRequestsOfCustomerForBooking(customerIdAsInt, (int) fieldId, slotHelper.NumOfDaysAvailableForBooking));

                        //Get customer's already approved bookings
                        var approvedBookings = recordRepo.GetApprovedRecordsForBooking((int)fieldId, slotHelper.NumOfDaysAvailableForBooking);
                        ApprovedSlots = slotHelper.getListOfUnavailableSlotsInInt(approvedBookings);

                        //Get slots that is before the booking time
                        UnavailableSlots = slotHelper.GetUnavailableSlotsNow();
                    }
                    catch (Exception ex)
                    {
                        return NotFound(ex.Message);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("BookingField/Request: Hasn't Login Yet!");
                    return NotFound();
                }
            }
            
            return Page();
        }        

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model Invalid! Page: BookingField/Request - Method: POST");
                return Page();
            }

            if (ChosenSlots != null && ChosenSlots.Count != 0)
            {
                List<BookingRequest> reqs = new List<BookingRequest>();
                int customerId;

                // check if Session is still up
                if (HttpContext.Session.GetString("CustomerId") == null) return RedirectToPage("/Index");
                else
                {
                    customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                }

                // check if slot requested is not booked or already requested before by customer

                var approvedBookings = recordRepo.GetApprovedRecordsForBooking(CurrFieldId, slotHelper.NumOfDaysAvailableForBooking);
                ApprovedSlots = slotHelper.getListOfUnavailableSlotsInInt(approvedBookings);

                CurrCustomerPendingSlots = slotHelper.getListOfUnavailableSlotsInInt(reqRepo.GetPendingRequestsOfCustomerForBooking(customerId, CurrFieldId, slotHelper.NumOfDaysAvailableForBooking));

                foreach (int slot in ChosenSlots)
                {
                    foreach (int bookedSlot in ApprovedSlots)
                    {
                        if (bookedSlot == slot) return NotFound();
                        System.Diagnostics.Debug.WriteLine("Approved Slots");
                    }

                    foreach (int pendingSlot in CurrCustomerPendingSlots)
                    {
                        if (pendingSlot == slot) return NotFound();
                        System.Diagnostics.Debug.WriteLine("Pending Slots");
                    }
                }

                foreach (int slot in ChosenSlots)
                {
                    BookingRequest req = new BookingRequest();

                    //req.Id = 1;
                    req.FieldId = CurrFieldId;
                    req.CustomerId = customerId;
                    req.SendDate = DateTime.Now;
                    req.StartTime = slotHelper.BookingSlots[slot];
                    req.EndTime = slotHelper.BookingSlots[slot].AddHours(1); // 1 slot = 1 hour                                        

                    reqs.Add(req);
                }

                reqRepo.AddCustomerRequests(reqs);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Request POST - Empty List");
                return RedirectToPage("/BookingField/Index");
            }

            System.Diagnostics.Debug.WriteLine("Request POST - Success!");
            return RedirectToPage("/BookingField/RequestHistory");
        }
    }
}
