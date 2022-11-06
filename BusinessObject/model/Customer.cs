﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using BusinessObject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
    public partial class Customer
    {
        public Customer()
        {
            BlackList = new HashSet<BlackList>();
            BookingRequest = new HashSet<BookingRequest>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of password is from 8 to 50 characters")]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The length of name is from 5 to 50 characters")]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DOBDateValidation]
        public DateTime Dob { get; set; }
        [Required]
        public bool Status { get; set; }

        public virtual ICollection<BlackList> BlackList { get; set; }
        public virtual ICollection<BookingRequest> BookingRequest { get; set; }
    }
}