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
        public Guid? StaffId { get; }
        public DateTime? CalendarDay { get; }
        public int DayOfWeek { get; }

        public bool TimeSlotSelectable { get; }
        
        public IReadOnlyCollection<WorkHours> WorkHours => _workHours;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        private ScheduleDay(DayOfWeek dayOfWeek, List<WorkHours> workHours, Guid serviceId, Guid? staffId) 
        {
            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.DayOfWeek = (int)dayOfWeek;
            this._workHours = workHours;
        }

        private ScheduleDay(DateTime calendarDay, List<WorkHours> workHours, Guid serviceId, Guid? staffId)
        {
            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.CalendarDay = calendarDay;
            this.DayOfWeek = (int)calendarDay.Date.DayOfWeek;
            this._workHours = workHours;
        }

        /// <summary>
        /// Create reccuring schedule for day of the week.
        /// </summary>
        /// <param name="dayOfWeek">Weekly reccuring schedule day.</param>
        /// <param name="workHours">Work hours for the day.</param>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="staffId">If specified, schedule is bound to one service staff member.</param>
        /// <returns></returns>
        public static ScheduleDay CreateSchedule(DayOfWeek dayOfWeek, List<WorkHours> workHours, Guid serviceId, Guid? staffId) 
        {
            return new ScheduleDay(dayOfWeek, workHours, serviceId, staffId);
        }

        /// <summary>
        /// Create one time schedule. Overrides reccuring week day schedule.
        /// </summary>
        /// <param name="calendarDay">Calendar day for the schedule.</param>
        /// <param name="workHours">Workhours for the day.</param>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="staffId">If specified, schedule is bound to one service staff member.</param>
        /// <returns></returns>
        public static ScheduleDay CreateSchedule(DateTime calendarDay, List<WorkHours> workHours, Guid serviceId, Guid? staffId)
        {
            return new ScheduleDay(calendarDay, workHours, serviceId, staffId);
        }

        public void UpdateScheduleDay(List<WorkHours> workHours)
        {
            this._workHours.Clear();
            this._workHours.AddRange(workHours);
        }

        //public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        //{
        //    this.CheckBusinessRule(new AppointmentCannotBeAddedToClosedSchedule(this.Open));
        //    this.CheckBusinessRule(new AppointmentTimeSlotInWorkHours(this._workHours, appointmentTimeSlot));
        //    this.CheckBusinessRule(new AppointmentTimeSlotNotOverlapping(this._appointments, appointmentTimeSlot));

        //    this._appointments.Add(Appointment.CreateNew(appointmentTimeSlot, contactInformation));
        //}
    }
}
