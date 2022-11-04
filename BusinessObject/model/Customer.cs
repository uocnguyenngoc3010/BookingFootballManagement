﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObject.Model
{
    public partial class Customer
    {
        public Customer()
        {
            BlackList = new HashSet<BlackList>();
            BookingRequest = new HashSet<BookingRequest>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<BlackList> BlackList { get; set; }
        public virtual ICollection<BookingRequest> BookingRequest { get; set; }
    }
}