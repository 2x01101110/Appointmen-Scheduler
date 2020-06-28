using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Domain.Schedule
{
    public class Reccurence : ValueObject
    {
        public int? Year { get; }
        public Month? Month { get; }
        public int? Week { get; }
        public Day DayOfTheWeek { get; }

        public static Reccurence Never(int year, Month month, int week, Day dayOfTheWeek) 
        {
            return new Reccurence(year, month, week, dayOfTheWeek);
        }
        public static Reccurence Weekly(Day dayOfTheWeek)
        {
            return new Reccurence(dayOfTheWeek);
        }

        private Reccurence(int year, Month month, int week, Day dayOfTheWeek)
        {
            this.Year = year;
            this.Month = month;
            this.Week = week;
            this.DayOfTheWeek = dayOfTheWeek;
        }
        private Reccurence(Day dayOfTheWeek)
        {
            this.DayOfTheWeek = dayOfTheWeek;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Year;
            yield return this.Month;
            yield return this.Week;
            yield return this.DayOfTheWeek;
        }
    }
}
