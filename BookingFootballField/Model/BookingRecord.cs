using System;
using System.Collections.Generic;

#nullable disable

namespace BookingFootballField.Model
{
    public partial class BookingRecord
    {
        public int Id { get; set; }
        public int BookingRequestId { get; set; }
        public int StaffId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        public virtual BookingRequest BookingRequest { get; set; }
        public virtual staff Staff { get; set; }
    }
}
