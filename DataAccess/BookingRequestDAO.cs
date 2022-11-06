using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingRequestDAO
    {
        private static BookingRequestDAO instance = null;
        private static readonly object instanceLock = new object();
        private FBookingDBContext _context;
        private BookingRequestDAO()
        {
            _context = new FBookingDBContext();
        }
        public static BookingRequestDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingRequestDAO();
                    }
                    return instance;
                }
            }
        }
        public string PENDING_STATUS
        {
            get { return "PENDING"; }
        }
        public string APPROVED_STATUS
        {
            get { return "APPROVED"; }
        }
        public string DECLINED_STATUS
        {
            get { return "DECLINED"; }
        }
        public string CANCELED_STATUS
        {
            get { return "CANCELED"; }
        }

        public List<BookingRequest> GetAllRequests()
        {
            RefreshPendingRequests();

            List<BookingRequest> requests = new List<BookingRequest>();

            requests = _context.BookingRequests.Include(r => r.Customer).OrderByDescending(r => r.Id).ToList();

            return requests;
        }

        // Get all Booking Requests from the beginning of the current day to then end of the (numOfDays)th day.
        // Provide to user which slots of a day for a field they can book it.
        public List<DateTime> GetPendingRequestsForBooking(int fieldId, int numOfDays)
        {
            RefreshPendingRequests();

            List<DateTime> requests = new List<DateTime>();

            requests = _context.BookingRequests.Where(r => r.FieldId == fieldId && r.StartTime.Date > DateTime.Now.Date && r.StartTime.Date <= DateTime.Now.Date.AddDays(numOfDays))
                                            .OrderBy(r => r.StartTime).Select(r => r.StartTime).ToList();

            return requests;
        }

        public BookingRequest GetRequestById(int requestId)
        {
            RefreshPendingRequests();

            BookingRequest req = null;

            try
            {
                req = _context.BookingRequests.SingleOrDefault(r => r.Id == requestId);

                if (req == null) throw new Exception("Stupid Id! (in bookingRequestRepo getRequestById)");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return req;
        }

        public List<DateTime> GetPendingRequestsOfCustomerForBooking(int customerId, int fieldId, int numOfDays)
        {
            RefreshPendingRequests();

            List<DateTime> reqs = null;

            reqs = _context.BookingRequests.Where(r => r.CustomerId == customerId && r.FieldId == fieldId && r.StartTime.CompareTo(DateTime.Now) > 0 && r.StartTime.Date.CompareTo(DateTime.Now.AddDays(numOfDays).Date) <= 0 && r.Status.Equals(PENDING_STATUS))
                                            .OrderBy(r => r.StartTime).Select(r => r.StartTime).ToList();

            return reqs;
        }

        public void AddCustomerRequests(List<BookingRequest> reqs)
        {
            RefreshPendingRequests();

            foreach (BookingRequest req in reqs)
            {
                req.Status = PENDING_STATUS;
                _context.BookingRequests.Add(req);
            }

            _context.SaveChanges();
        }

        // Only "PENDING" requests
        public List<BookingRequest> GetPendingRequestsByCustomerId(int customerId)
        {
            RefreshPendingRequests();

            List<BookingRequest> reqs = null;

            reqs = _context.BookingRequests.Include(r => r.Field)
                                    .Where(r => r.CustomerId == customerId && r.Status.Equals(PENDING_STATUS))
                                    .OrderByDescending(r => r.SendDate).ThenByDescending(r => r.Id).ToList();

            System.Diagnostics.Debug.WriteLine("BookingRequestRepository - GetPendingRequestsByCustomerId - NumOfReqs: " + reqs.Count);
            System.Diagnostics.Debug.WriteLine("-----------------------------------------------------------------------");

            return reqs;
        }

        // Both "APPROVED" & "DECLINED" requests
        public List<BookingRequest> GetResolvedRequestsByCustomerId(int customerId)
        {
            RefreshPendingRequests();

            List<BookingRequest> reqs = null;

            reqs = _context.BookingRequests.Include(r => r.Field)
                                        .Where(r => r.CustomerId == customerId && (r.Status.Equals(APPROVED_STATUS) || r.Status.Equals(DECLINED_STATUS)))
                                        .OrderByDescending(r => r.SendDate).ThenByDescending(r => r.Id).ToList();

            System.Diagnostics.Debug.WriteLine("BookingRequestRepository - GetResolvedRequestsByCustomerId - NumOfReqs: " + reqs.Count);
            System.Diagnostics.Debug.WriteLine("-----------------------------------------------------------------------");

            return reqs;
        }

        public List<BookingRequest> GetAllRequestsByCustomerId(int customerId)
        {
            RefreshPendingRequests();

            List<BookingRequest> reqs = null;

            reqs = _context.BookingRequests.Include(r => r.Field)
                .Where(r => r.CustomerId == customerId).OrderByDescending(r => r.SendDate).ThenByDescending(r => r.Id).ToList();

            System.Diagnostics.Debug.WriteLine("BookingRequestRepository - GetAllRequestsByCustomerId - NumOfReqs: " + reqs.Count);
            System.Diagnostics.Debug.WriteLine("-----------------------------------------------------------------------");

            return reqs;
        }

        public bool CheckValidRequestForCanceling(int customerId, int requestId)
        {
            // That request must be PENDING in order to be canceled
            var req = _context.BookingRequests.FirstOrDefault(r => r.CustomerId == customerId && r.Id == requestId && r.Status.Equals(PENDING_STATUS));

            if (req != null) return true;
            else return false;
        }

        public void CustomerCancelsARequest(int requestId)
        {
            RefreshPendingRequests();

            var req = _context.BookingRequests.FirstOrDefault(r => r.Id == requestId);

            if (req == null)
            {
                System.Diagnostics.Debug.WriteLine("BookingField/CancelARequest: For safety sake this is not gonna happen and if it happen then i don't even know");
                return;
            }
            else
            {
                req.Status = CANCELED_STATUS;
                _context.BookingRequests.Update(req);
                _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine("BookingField/CancelARequest: Request " + requestId + " has been canceled!");
            }
        }

        public List<BookingRequest> GetPendingRequestsOfCustomersForResolving(int numOfDays)
        {
            RefreshPendingRequests();

            List<BookingRequest> reqs;

            reqs = _context.BookingRequests.Where(r => r.Status.Equals(PENDING_STATUS) && r.StartTime.CompareTo(DateTime.Now) > 0 && r.StartTime.CompareTo(DateTime.Now.AddDays(numOfDays).Date) <= 0)
                                            .Include(r => r.Customer).OrderBy(r => r.SendDate).ToList();

            return reqs;
        }

        public void DeclineARequest(int requestId)
        {
            RefreshPendingRequests();

            BookingRequest req;

            req = _context.BookingRequests.FirstOrDefault(r => r.Id == requestId);

            if (req == null)
            {
                System.Diagnostics.Debug.WriteLine("BookingField/DeclineARequest: For safety sake this is not gonna happen and if it happen then i don't even know");
            }
            else if (!req.Status.Equals(PENDING_STATUS))
            {
                System.Diagnostics.Debug.WriteLine("BookingField/DeclineARequest: For safety sake this is not gonna happen and if it happen then i don't even know");
            }
            else
            {
                req.Status = DECLINED_STATUS;
                _context.BookingRequests.Update(req);
                _context.SaveChanges();
            }
        }

        public void ApproveARequest(int requestId)
        {
            RefreshPendingRequests();

            BookingRequest req;

            req = _context.BookingRequests.FirstOrDefault(r => r.Id == requestId);

            if (req == null)
            {
                System.Diagnostics.Debug.WriteLine("BookingField/DeclineARequest: For safety sake this is not gonna happen and if it happen then i don't even know");
            }
            else if (!req.Status.Equals(PENDING_STATUS))
            {
                System.Diagnostics.Debug.WriteLine("BookingField/DeclineARequest: For safety sake this is not gonna happen and if it happen then i don't even know");
            }
            else
            {
                req.Status = APPROVED_STATUS;
                _context.BookingRequests.Update(req);

                var sameReqs = _context.BookingRequests.Where(r => r.Status.Equals(PENDING_STATUS) && r.StartTime.CompareTo(req.StartTime) == 0 && r.Id != req.Id).ToList();
                foreach (var r in sameReqs)
                {
                    r.Status = CANCELED_STATUS;
                    _context.BookingRequests.Update(r);
                }

                _context.SaveChanges();
            }
        }

        // Cancel all pending requests that have passed the designated time without being resolved
        private void RefreshPendingRequests()
        {
            List<BookingRequest> reqs = _context.BookingRequests.Where(r => r.Status.Equals(PENDING_STATUS)).ToList();

            foreach (var req in reqs)
            {
                if (req.StartTime.CompareTo(DateTime.Now) <= 0)
                {
                    req.Status = CANCELED_STATUS;
                    System.Diagnostics.Debug.WriteLine(DateTime.Now + " - Request ID " + req.Id + " has been canceled by the system!");
                }
            }

            _context.BookingRequests.UpdateRange(reqs);
            _context.SaveChanges();
        }

        public bool IsApprovedRequestCancelable(int requestID)
        {
            var req = _context.BookingRequests.FirstOrDefault(r => r.Id == requestID);
            if (req == null) return false;
            else
            {
                if (req.Status.Equals(APPROVED_STATUS) && DateTime.Now.Date.AddDays(3).CompareTo(req.StartTime.Date) <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void CancelApprovedRequest(int requestID)
        {
            var req = GetRequestById(requestID);
            if (req == null)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + " - CancelApprovedRequest - Booking Request DAO - Can't find Request ID " + requestID);
                return;
            }
            else
            {
                if (IsApprovedRequestCancelable(requestID))
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now + " - CancelApprovedRequest - Booking Request DAO - Request ID " + req.Id);
                    req.Status = CANCELED_STATUS;
                    _context.BookingRequests.Update(req);
                    _context.SaveChanges();
                }
            }
        }
    }
}
