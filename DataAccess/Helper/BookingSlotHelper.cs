using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helper
{
    public class BookingSlotHelper
    {
        private static int numOfDaysAvailableForBooking = 14;
        public int NumOfDaysAvailableForBooking
        {
            get { return numOfDaysAvailableForBooking; }
        }

        private static int numOfSlotsPerDay = 12;
        public int NumOfSlotPerDay
        {
            get { return numOfSlotsPerDay; }
        }
        private Dictionary<int, string> timeSlots = new Dictionary<int, string>();
        public Dictionary<int, string> TimeSlots
        {
            get { return timeSlots; }
        }

        // Booking only starts from 6AM
        private static int openingTime = 6;

        // Supposedly this list has to have 28(days) * 12(slots per day) = 336 elements;
        public List<DateTime> BookingSlots { get; set; } = new List<DateTime>();

        public BookingSlotHelper()
        {
            DateTime openingSlot = DateTime.MinValue;
            //Because DateTime.MinValue starts at 1/1/0001 minus everytime time related value by 1
            openingSlot = openingSlot.AddYears(DateTime.Now.Year - 1);
            openingSlot = openingSlot.AddMonths(DateTime.Now.Month - 1);
            openingSlot = openingSlot.AddDays(DateTime.Now.Day - 1);
            openingSlot = openingSlot.AddHours(openingTime);

            DateTime currSlot = openingSlot;

            // loop for 14 days
            for (int i = 0; i < numOfDaysAvailableForBooking; i++)
            {
                // reset hour back to 6AM and increments day by the current loop
                currSlot = openingSlot.AddDays(i);

                BookingSlots.Add(currSlot);

                // increment 1 hour for 11 times => the first slot is added prematurely & number of slots per day is 12
                for (int j = 1; j < numOfSlotsPerDay; j++)
                {
                    currSlot = currSlot.AddHours(1);
                    BookingSlots.Add(currSlot);
                }
            }

            for (int i = 0; i < numOfSlotsPerDay; i++)
            {
                string time = "(" + BookingSlots[i].ToShortTimeString() + " - " + BookingSlots[i].AddHours(1).ToShortTimeString() + ")";
                timeSlots.Add(i + 1, time);
            }
        }

        public List<int> GetUnavailableSlotsNow()
        {
            List<int> results = new List<int>();

            int countSlot = 0;

            foreach (var slot in BookingSlots)
            {
                // slot is before the booking time
                if (slot.CompareTo(DateTime.Now) < 0)
                {
                    results.Add(countSlot);
                    System.Diagnostics.Debug.WriteLine(countSlot + " - " + slot);
                }

                countSlot++;
            }

            return results;
        }

        public List<int> getListOfUnavailableSlotsInInt(List<DateTime> unavailableSlots)
        {
            List<int> unavailableSlotsInInt = new List<int>();

            int countSlot = 0;

            foreach (DateTime slot in BookingSlots)
            {
                foreach (DateTime sl in unavailableSlots)
                {
                    // check if they are the same                    
                    if (sl.CompareTo(slot) == 0)
                    {
                        unavailableSlotsInInt.Add(countSlot);
                    }
                }

                countSlot++;
            }

            return unavailableSlotsInInt;
        }
    }
}
