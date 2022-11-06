using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingRequestRepository : IBookingRequestRepository
    {
        public List<BookingRequest> GetAllRequests() => BookingRequestDAO.Instance.GetAllRequests();
        public List<BookingRequest> GetPendingRequestsByCustomerId(int customerId) => BookingRequestDAO.Instance.GetPendingRequestsByCustomerId(customerId);
        public List<BookingRequest> GetResolvedRequestsByCustomerId(int customerId) => BookingRequestDAO.Instance.GetResolvedRequestsByCustomerId(customerId);
        public List<BookingRequest> GetAllRequestsByCustomerId(int customerId) => BookingRequestDAO.Instance.GetAllRequestsByCustomerId(customerId);
        public List<BookingRequest> GetPendingRequestsOfCustomersForResolving(int numOfDays) => BookingRequestDAO.Instance.GetPendingRequestsOfCustomersForResolving(numOfDays);
        public List<DateTime> GetPendingRequestsOfCustomerForBooking(int customerId, int fieldId, int numOfDays) => BookingRequestDAO.Instance.GetPendingRequestsOfCustomerForBooking(customerId, fieldId, numOfDays);
        public BookingRequest GetRequestById(int requestId) => BookingRequestDAO.Instance.GetRequestById(requestId);
        public bool CheckValidRequestForCanceling(int customerId, int requestId) => BookingRequestDAO.Instance.CheckValidRequestForCanceling(customerId, requestId);
        public bool IsApprovedRequestCancelable(int requestID) => BookingRequestDAO.Instance.IsApprovedRequestCancelable(requestID);
        public void AddCustomerRequests(List<BookingRequest> reqs) => BookingRequestDAO.Instance.AddCustomerRequests(reqs);
        public void CustomerCancelsARequest(int requestId) => BookingRequestDAO.Instance.CustomerCancelsARequest(requestId);
        public void DeclineARequest(int requestId) => BookingRequestDAO.Instance.DeclineARequest(requestId);
        public void ApproveARequest(int requestId) => BookingRequestDAO.Instance.ApproveARequest(requestId);
        public void CancelApprovedRequest(int requestID) => BookingRequestDAO.Instance.CancelApprovedRequest(requestID);
    }
}
