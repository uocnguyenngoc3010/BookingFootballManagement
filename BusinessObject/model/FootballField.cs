using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Model
{
    public partial class FootballField
    {
        public FootballField()
        {
            BookingRequests = new HashSet<BookingRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }

        public virtual ICollection<BookingRequest> BookingRequests { get; set; }
    }
}
