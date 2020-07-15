using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDays.Rules;
using System;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDays
{
    public class ScheduleDay : Entity<Guid>, IAggregateRoot 
    {
        private readonly List<WorkHours> _workHours;
        private readonly List<Appointment> _appointments;

        public Guid ServiceId { get; }
        public DateTime? CalendarDay { get; }
        public int DayOfWeek { get; }
        public bool Open { get; }
        public IReadOnlyCollection<WorkHours> WorkHours => _workHours;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        private ScheduleDay(
            DayOfWeek dayOfWeek, bool open, List<WorkHours> workHours, Guid serviceId) 
        {
            this.ServiceId = serviceId;
            this.DayOfWeek = (int)dayOfWeek;
            this.Open = open;
            this._workHours = workHours;
        }
        private ScheduleDay(
            DateTime calendarDay, bool open, List<WorkHours> workHours, Guid serviceId)
        {
            this.ServiceId = serviceId;
            this.CalendarDay = calendarDay;
            this.DayOfWeek = (int)calendarDay.Date.DayOfWeek;
            this.Open = open;
            this._workHours = workHours;
        }

        /// <summary>
        /// Reccuring schedule - a schedule that keeps repeating every week
        /// </summary>
        public static ScheduleDay CreateSchedule(
            DayOfWeek dayOfWeek, bool open, List<WorkHours> workHours, Guid serviceId) 
        {
            return new ScheduleDay(dayOfWeek, open, workHours, serviceId);
        }

        /// <summary>
        /// Schedule for one day - for example when there's Christmass
        /// </summary>
        public static ScheduleDay CreateSchedule(
            DateTime calendarDay, bool open, List<WorkHours> workHours, Guid serviceId)
        {
            return new ScheduleDay(calendarDay, open, workHours, serviceId);
        }

        public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            this.CheckBusinessRule(new AppointmentCannotBeAddedToClosedSchedule(this.Open));
            this.CheckBusinessRule(new AppointmentTimeSlotInWorkHours(this._workHours, appointmentTimeSlot));
            this.CheckBusinessRule(new AppointmentTimeSlotNotOverlapping(this._appointments, appointmentTimeSlot));

            this._appointments.Add(Appointment.CreateNew(appointmentTimeSlot, contactInformation));
        }
    }
}
