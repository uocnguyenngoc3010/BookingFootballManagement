﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace BookingFieldManagement.Pages.FootballField
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IFootballFieldRepository footballFieldRepository = new FootballFieldRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string name;
        public CreateModel(BusinessObject.Model.FBookingDBContext context)
        {
            _context = context;
        }
        public string staffId;
        public string isAdmin;
        public string customerId;
        public IActionResult OnGet()
        {
            
            
            customerId = HttpContext.Session.GetString("CustomerId");
            staffId = HttpContext.Session.GetString("StaffId");
            isAdmin = HttpContext.Session.GetString("isAdmin");
            if (staffId == null && isAdmin == null)
            {
                return RedirectToPage("/Index");
            }
            if (staffId != null)
            {
                name = staffRepository.GetName(Int32.Parse(staffId));
            }
            return Page();
        }

        [BindProperty]
        public BusinessObject.Model.FootballField FootballField { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.FootballFields.Add(FootballField);
            //await _context.SaveChangesAsync();
            footballFieldRepository.Add(FootballField);

            return RedirectToPage("./Index");
        }
    }
}
