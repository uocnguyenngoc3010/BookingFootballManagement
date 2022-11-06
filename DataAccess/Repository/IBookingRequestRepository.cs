using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookingRequestRepository
    {
        public List<BookingRequest> GetAllRequests();
        public List<BookingRequest> GetPendingRequestsByCustomerId(int customerId);
        public List<BookingRequest> GetResolvedRequestsByCustomerId(int customerId);
        public List<BookingRequest> GetAllRequestsByCustomerId(int customerId);
        public List<BookingRequest> GetPendingRequestsOfCustomersForResolving(int numOfDays);
        public List<DateTime> GetPendingRequestsOfCustomerForBooking(int customerId, int fieldId, int numOfDays);
        public BookingRequest GetRequestById(int requestId);
        public bool CheckValidRequestForCanceling(int customerId, int requestId);
        public bool IsApprovedRequestCancelable(int requestID);
        public void AddCustomerRequests(List<BookingRequest> reqs);
        public void CustomerCancelsARequest(int requestId);
        public void DeclineARequest(int requestId);
        public void ApproveARequest(int requestId);
        public void CancelApprovedRequest(int requestID);
    }
}
