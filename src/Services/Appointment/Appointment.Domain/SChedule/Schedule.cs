using BuildingBlocks.Domain;

namespace Appointment.Domain.Schedule
{
    public class Schedule : Entity<int>, IAggregateRoot
    {
        public Reccurence Reccurence { get; private set; }
        public bool Available { get; private set; }
        public int OpeningHours { get; private set; }
        public int ClosingHours { get; private set; }

        private Schedule(
            Reccurence reccurence,
            bool available, 
            int openingHours, 
            int closingHours)
        {
            this.Reccurence = reccurence;
            this.Available = available;
            this.OpeningHours = openingHours;
            this.ClosingHours = closingHours;
        }

        public static Schedule Create(
            Reccurence reccurence,
            bool available, 
            int openingHours, 
            int closingHours)
        {
            return new Schedule(reccurence, available, openingHours, closingHours);
        }
    }

    public class test
    {
        public test()
        {
            var weekly = Schedule.Create(Reccurence.Weekly(Day.Friday), true, 520, 1020);
            var holiday = Schedule.Create(Reccurence.Never(2020, Month.April, 4, Day.Friday), false, 0, 0);
        }
    }
}
