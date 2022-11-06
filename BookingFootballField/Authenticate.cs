using BusinessObject.model;
using BusinessObject.Model;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BookingFootballField
{
    public class Authenticate
    {
        public Authenticate() { }

        public bool IsAdmin(string email, string password)
        {
            bool isAdmin = false;
            var json = File.ReadAllText("appsettings.json");
            var adminAccount = JsonSerializer.Deserialize<AdminAccount>(json, null);
            if (email.Equals(adminAccount.Email) && password.Equals(adminAccount.Password))
            {
                isAdmin = true;
            }
            return isAdmin;
        }

        public staff LoginByStaff(string email, string password)
        {
            staff staff = new staff();
            var _context = new FBookingDBContext();
            staff = _context.staff.Where(c => c.Email.Equals(email) && c.Password.Equals(password)).SingleOrDefault();
            return staff;
        }

        public Customer LoginByCustomer(string email, string password)
        {
            Customer customer = new Customer();
            var _context = new FBookingDBContext();
            customer = _context.Customers.Where(c => c.Email.Equals(email) && c.Password.Equals(password)).SingleOrDefault();
            return customer;
        }
    }
}

