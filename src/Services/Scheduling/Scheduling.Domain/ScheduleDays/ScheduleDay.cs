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

        public static ScheduleDay CreateSchedule(DayOfWeek dayOfWeek, List<WorkHours> workHours, Guid serviceId, Guid? staffId) 
        {
            return new ScheduleDay(dayOfWeek, workHours, serviceId, staffId);
        }

        public static ScheduleDay CreateSchedule(DateTime calendarDay, List<WorkHours> workHours, Guid serviceId, Guid? staffId)
        {
            return new ScheduleDay(calendarDay, workHours, serviceId, staffId);
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
