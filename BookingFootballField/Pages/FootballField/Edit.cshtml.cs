using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookingFieldManagement.Pages.FootballField
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly BusinessObject.Model.FBookingDBContext _context;
        IFootballFieldRepository footballFieldRepository = new FootballFieldRepository();
        IStaffRepository staffRepository = new StaffRepository();
        public string name;
        public EditModel(BusinessObject.Model.FBookingDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public string staffId;
        public string isAdmin;
        public string customerId;
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        [BindProperty]
        public string Imgpath { get; set; }
        [BindProperty]
        public BusinessObject.Model.FootballField FootballField { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null)
            {
                return NotFound();
            }

            //FootballField = await _context.FootballFields.FirstOrDefaultAsync(m => m.Id == id);
            FootballField = footballFieldRepository.GetByID((int)id);
            if (FootballField == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(FootballField).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();

                var file = Path.Combine(webHostEnvironment.WebRootPath, @"image", FileUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    FileUpload.CopyTo(fileStream);
                    Imgpath = @"\image\" + FileUpload.FileName;


                }
                FootballField.Image = Imgpath;
                footballFieldRepository.Update(FootballField);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootballFieldExists(FootballField.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FootballFieldExists(int id)
        {
            var footballField = footballFieldRepository.GetByID(id);
            if (footballField != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
