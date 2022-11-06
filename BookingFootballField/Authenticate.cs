using BusinessObject.model;
using BusinessObject.Model;
using System;
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
            try {
                bool isAdmin = false;
                var json = File.ReadAllText("appsettings.json");
                var adminAccount = JsonSerializer.Deserialize<AdminAccount>(json, null);
                if (email.Equals(adminAccount.Email) && password.Equals(adminAccount.Password))
                {
                    isAdmin = true;
                }
                return isAdmin;
            }
            catch { 
            return false;
            }   
            
        }

        public staff LoginByStaff(string email, string password)
        {
            try
            {
                staff staff = new staff();
                var _context = new FBookingDBContext();
                staff = _context.staff.Where(c => c.Email.Equals(email) && c.Password.Equals(password)).SingleOrDefault();
                return staff;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public Customer LoginByCustomer(string email, string password)
        {
            try
            {
                Customer customer = new Customer();
                var _context = new FBookingDBContext();
                customer = _context.Customers.Where(c => c.Email.Equals(email) && c.Password.Equals(password)).SingleOrDefault();
                return customer;
            } catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}

