using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingRecordDAO
    {
        private static BookingRecordDAO instance = null;
        private static readonly object instanceLock = new object();
        private FBookingDBContext _context;
        private BookingRecordDAO()
        {
            _context = new FBookingDBContext();
        }
        public static BookingRecordDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingRecordDAO();
                    }
                    return instance;
                }
            }
        }

        private readonly string APPROVED_STATUS = "APPROVED";
        private readonly string CANCELED_STATUS = "CANCELED";

        public List<BookingRecord> GetAllRecords()
        {
            List<BookingRecord> records = new List<BookingRecord>();

            records = _context.BookingRecords.Include(r => r.BookingRequest).Include(r => r.staff)
                                        .Include(r => r.BookingRequest.Customer).OrderByDescending(r => r.Id).ToList();

            System.Diagnostics.Debug.WriteLine("Debug Get All Records");
            foreach (var r in records)
            {
                System.Diagnostics.Debug.WriteLine(r.Id);
            }

            return records;
        }

        public List<BookingRecord> CustomerGetAllRecords(int customerId)
        {
            List<BookingRecord> records = new List<BookingRecord>();

            records = _context.BookingRecords.Include(r => r.BookingRequest).Include(r => r.BookingRequest.Field)
                                        .Where(r => r.Status.Equals(APPROVED_STATUS) && r.BookingRequest.CustomerId == customerId)
                                        .OrderByDescending(r => r.Id).ToList();

            System.Diagnostics.Debug.WriteLine("Debug Customer Get All Records");
            foreach (var r in records)
            {
                System.Diagnostics.Debug.WriteLine(r.Id);
            }

            return records;
        }

        // Get all Booking Requests from the beginning of the current day to then end of the (numOfDays)th day.
        // Provide to user which slots of a day for a field they can book it.
        // (not sure this will run tbh)
        public List<DateTime> GetApprovedRecordsForBooking(int fieldId, int numOfDays)
        {
            List<DateTime> records = new List<DateTime>();

            records = _context.BookingRecords.Include(r => r.BookingRequest)
                                            .Where(r => r.BookingRequest.FieldId == fieldId && r.BookingRequest.StartTime.CompareTo(DateTime.Now) >= 0 && r.BookingRequest.StartTime.CompareTo(DateTime.Now.Date.AddDays(numOfDays)) <= 0 && r.Status.Equals(APPROVED_STATUS))
                                            .OrderBy(r => r.BookingRequest.StartTime).Select(r => r.BookingRequest.StartTime).ToList();

            return records;
        }

        public BookingRecord GetRecordById(int recordId)
        {
            BookingRecord record = null;

            try
            {
                record = _context.BookingRecords.SingleOrDefault(r => r.Id == recordId);

                if (record == null) throw new Exception("Stupid Id (in bookingRecordRepo getRecordById)!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return record;
        }

        public List<DateTime> GetRecordsOfCustomerForBooking(int customerId, int fieldId)
        {
            List<DateTime> records = null;

            records = _context.BookingRecords.Include(r => r.BookingRequest).Where(r => r.BookingRequest.CustomerId == customerId && r.BookingRequest.FieldId == fieldId && r.Status.Equals(APPROVED_STATUS)).Select(r => r.BookingRequest.StartTime).ToList();

            return records;
        }

        public void AddARecord(BookingRecord record)
        {
            record.Status = APPROVED_STATUS;
            _context.BookingRecords.Add(record);
            _context.SaveChanges();
        }

        public void CancelApprovedRecord(int requestID)
        {
            var record = _context.BookingRecords.FirstOrDefault(r => r.BookingRequestId == requestID);
            if (record == null)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + " - CancelApprovedRecord - Booking Record DAO - Can't find record with Request ID " + requestID);
            }
            else
            {
                record.Status = CANCELED_STATUS;
                System.Diagnostics.Debug.WriteLine(DateTime.Now + " - CancelApprovedRecord - Booking Record DAO - Record ID " + record.Id);
                _context.BookingRecords.Update(record);
                _context.SaveChanges();
            }
        }
    }
}
