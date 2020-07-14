using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDayAggregate.Rules;
using System;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDayAggregate
{
    public class ScheduleDay : Entity<Guid>, IAggregateRoot 
    {
        private readonly List<WorkHours> _workHours;
        private readonly List<Appointment> _appointments;

        public DateTime? CalendarDay { get; }
        public DayOfWeek DayOfWeek { get; }
        public bool Open { get; }
        public IReadOnlyCollection<WorkHours> WorkHours => _workHours;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        private ScheduleDay(
            DayOfWeek dayOfWeek, bool open, List<WorkHours> workHours) 
        {
            this.DayOfWeek = dayOfWeek;
            this.Open = open;
            this._workHours = workHours;
        }
        private ScheduleDay(
            DateTime calendarDay, bool open, List<WorkHours> workHours)
        {
            this.CalendarDay = calendarDay;
            this.DayOfWeek = (DayOfWeek)((int)calendarDay.Date.DayOfWeek);
            this.Open = open;
            this._workHours = workHours;
        }

        public static ScheduleDay CreateWeeklyRecurringDaySchedule(
            DayOfWeek dayOfWeek, bool open, List<WorkHours> workHours) 
        {
            return new ScheduleDay(dayOfWeek, open, workHours);
        }
        public static ScheduleDay CreateOneTimeDaySchedule(
            DateTime calendarDay, bool open, List<WorkHours> workHours)
        {
            return new ScheduleDay(calendarDay, open, workHours);
        }

        public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            this.CheckBusinessRule(new AppointmentTimeSlotInWorkHours(this._workHours, appointmentTimeSlot));
            this.CheckBusinessRule(new AppointmentTimeSlotNotOverlapping(this._appointments, appointmentTimeSlot));

            this._appointments.Add(Appointment.CreateNew(appointmentTimeSlot, contactInformation));
        }
    }
}
