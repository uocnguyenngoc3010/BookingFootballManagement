using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingRecordRepository : IBookingRecordRepository
    {
        public List<BookingRecord> GetAllRecords() => BookingRecordDAO.Instance.GetAllRecords();
        public List<BookingRecord> CustomerGetAllRecords(int customerId) => BookingRecordDAO.Instance.CustomerGetAllRecords(customerId);
        public List<DateTime> GetRecordsOfCustomerForBooking(int customerId, int fieldId) => BookingRecordDAO.Instance.GetRecordsOfCustomerForBooking(customerId, fieldId);
        public List<DateTime> GetApprovedRecordsForBooking(int fieldId, int numOfDays) => BookingRecordDAO.Instance.GetApprovedRecordsForBooking(fieldId, numOfDays);
        public BookingRecord GetRecordById(int recordId) => BookingRecordDAO.Instance.GetRecordById(recordId);
        public void AddARecord(BookingRecord record) => BookingRecordDAO.Instance.AddARecord(record);
        public void CancelApprovedRecord(int requestID) => BookingRecordDAO.Instance.CancelApprovedRecord(requestID);
    }
}
