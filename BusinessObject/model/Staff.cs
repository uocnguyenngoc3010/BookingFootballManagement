﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class Staff
    {
        public Staff()
        {
            BookingRecord = new HashSet<BookingRecord>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActived { get; set; }

        public virtual ICollection<BookingRecord> BookingRecord { get; set; }
    }
}