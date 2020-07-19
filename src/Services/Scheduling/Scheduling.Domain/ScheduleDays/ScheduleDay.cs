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
        public bool ClientCanSelectTimeSlot { get; private set; }
        public IReadOnlyCollection<WorkHours> WorkHours => _workHours;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        #region Schedule day creation
        private ScheduleDay(Guid serviceId, Guid? staffId, DayOfWeek dayOfWeek, List<WorkHours> workHours, bool clientCanSelectTimeSlot) 
        {
            this.CheckBusinessRule(new WorkHoursNotOverlapping(workHours));

            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.DayOfWeek = (int)dayOfWeek;

            // Initilizing backing fields?
            this._workHours = workHours ?? new List<WorkHours>();
            this._appointments = _appointments ?? new List<Appointment>();

            this.ClientCanSelectTimeSlot = clientCanSelectTimeSlot;
        }

        private ScheduleDay(Guid serviceId, Guid? staffId, DateTime calendarDay, List<WorkHours> workHours, bool clientCanSelectTimeSlot)
        {
            this.CheckBusinessRule(new WorkHoursNotOverlapping(workHours));

            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.CalendarDay = calendarDay;
            this.DayOfWeek = (int)calendarDay.Date.DayOfWeek;

            // Initilizing backing fields?
            this._workHours = workHours ?? new List<WorkHours>();
            this._appointments = _appointments ?? new List<Appointment>();

            this.ClientCanSelectTimeSlot = clientCanSelectTimeSlot;
        }

        public static ScheduleDay CreateReccuringWeeklySchedule(
            Guid serviceId, Guid? staffId, DayOfWeek dayOfWeek, List<WorkHours> workHours, bool clientCanSelectTimeSlot) 
        {
            return new ScheduleDay(serviceId, staffId, dayOfWeek, workHours, clientCanSelectTimeSlot);
        }

        public static ScheduleDay CreateOneTimeSchedule(
            Guid serviceId, Guid? staffId, DateTime calendarDay, List<WorkHours> workHours, bool clientCanSelectTimeSlot)
        {
            return new ScheduleDay(serviceId, staffId, calendarDay, workHours, clientCanSelectTimeSlot);
        }
        #endregion

        public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            // NEED TO CHECK IF NEW APPOINTMENT TIME SLOT START TIME IS WITHIN DEFFINED work hour time slots
            // 09:00
            // 09:20
            // 09:40
            // CANNOT CREATE A NEW APPOINTMENT @ 09:30

            var scheduleCalendarDay = this.CalendarDay ?? DateTime.UtcNow.Date;

            // Check if passed appointment day matches selected calendar day of the schedule
            if (appointmentTimeSlot.AppointmentDay != scheduleCalendarDay)
            {
                throw new Exception("Invalid calendar day provided for creation of appointment.");
            }

            // Client can select time slot and client has provided time slot
            if (this.ClientCanSelectTimeSlot && appointmentTimeSlot.AppointmentStart != null)
            {
                // Check if appointment is within working hours
                this.CheckBusinessRule(new AppointmentTimeSlotInWorkHours(this._workHours, appointmentTimeSlot));
                // Check if no overlapping schedules
                this.CheckBusinessRule(new AppointmentTimeSlotNotOverlapping(this._workHours, this._appointments, appointmentTimeSlot));

                Appointment newAppointment =
                    Appointment.CreateAppointmentWithTimeSlot(scheduleCalendarDay, appointmentTimeSlot, contactInformation);

                this._appointments.Add(newAppointment);
            }
            // Client cannot select time slot and client has not provided time slot
            else if (!this.ClientCanSelectTimeSlot && appointmentTimeSlot.AppointmentStart == null)
            {
                Appointment newAppointment =
                    Appointment.CreateAppointmentWithoutTimeSlot(scheduleCalendarDay, contactInformation);

                this._appointments.Add(newAppointment);
            }
            // Invalid appointment creation request
            else 
            {
                throw new ArgumentException($"Placeholder exception. Cannot create appointment.");
            }
        }
        
        public void UpdateScheduleDay(List<WorkHours> workHours, bool clientCanSelectTimeSlot)
        {
            this._workHours.Clear();
            this._workHours.AddRange(workHours);

            this.ClientCanSelectTimeSlot = clientCanSelectTimeSlot;
        }

        // DateTime AppointmentDay   
        // int? AppointmentStart
        // 1) always check if AppointmentStart (if not null) is in work hours
        //      1.1) If WorkHours.AppointmentLength is not null - check if AppointmentTimeSlot.AppointmentStart !> WorkHours.WorkHoursEnd + WorkHours.AppointmentLenght
    }
}
