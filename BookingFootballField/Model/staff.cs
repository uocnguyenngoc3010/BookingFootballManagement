using System;
using System.Collections.Generic;

#nullable disable

namespace BookingFootballField.Model
{
    public partial class staff
    {
        public staff()
        {
            BookingRecords = new HashSet<BookingRecord>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActived { get; set; }

        public virtual ICollection<BookingRecord> BookingRecords { get; set; }
    }
}
