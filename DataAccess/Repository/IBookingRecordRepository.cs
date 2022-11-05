using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookingRecordRepository
    {
        public List<BookingRecord> GetAllRecords();
        public List<BookingRecord> CustomerGetAllRecords(int customerId);
        public List<DateTime> GetRecordsOfCustomerForBooking(int customerId, int fieldId);
        public List<DateTime> GetApprovedRecordsForBooking(int fieldId, int numOfDays);
        public BookingRecord GetRecordById(int recordId);
        public void AddARecord(BookingRecord record);
        public void CancelApprovedRecord(int requestID);
    }
}
