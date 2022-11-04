using System;
using System.Collections.Generic;

#nullable disable

namespace BookingFootballField.Model
{
    public partial class Customer
    {
        public Customer()
        {
            BlackLists = new HashSet<BlackList>();
            BookingRequests = new HashSet<BookingRequest>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<BlackList> BlackLists { get; set; }
        public virtual ICollection<BookingRequest> BookingRequests { get; set; }
    }
}
