using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Model
{
    public partial class BookingRequest
    {
        public BookingRequest()
        {
            BookingRecords = new HashSet<BookingRecord>();
        }

        public int Id { get; set; }
        public int FieldId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual FootballField Field { get; set; }
        public virtual ICollection<BookingRecord> BookingRecords { get; set; }
    }
}
