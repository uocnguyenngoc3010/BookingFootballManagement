using System;
using System.Collections.Generic;

#nullable disable

namespace BookingFootballField.Model
{
    public partial class BlackList
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime BanDate { get; set; }
        public string Reason { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
