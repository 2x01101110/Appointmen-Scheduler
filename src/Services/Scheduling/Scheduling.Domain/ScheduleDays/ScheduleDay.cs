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

        private ScheduleDay(Guid serviceId, Guid? staffId, DayOfWeek dayOfWeek, List<WorkHours> workHours, bool clientCanSelectTimeSlot) 
        {
            this.CheckBusinessRule(new WorkHoursNotOverlapping(workHours));

            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.DayOfWeek = (int)dayOfWeek;
            this._workHours = workHours;
            this.ClientCanSelectTimeSlot = clientCanSelectTimeSlot;
        }

        private ScheduleDay(Guid serviceId, Guid? staffId, DateTime calendarDay, List<WorkHours> workHours, bool clientCanSelectTimeSlot)
        {
            this.CheckBusinessRule(new WorkHoursNotOverlapping(workHours));

            this.ServiceId = serviceId;
            this.StaffId = staffId;
            this.CalendarDay = calendarDay;
            this.DayOfWeek = (int)calendarDay.Date.DayOfWeek;
            this._workHours = workHours;
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

        public void UpdateScheduleDay(List<WorkHours> workHours, bool clientCanSelectTimeSlot)
        {
            this._workHours.Clear();
            this._workHours.AddRange(workHours);

            this.ClientCanSelectTimeSlot = clientCanSelectTimeSlot;
        }

        public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            // NOTE!!!
            // WHAT IF CLIENT CAN SELECT TIME AND AppointmentTimeSlot.AppointmentStart AND AppointmentTimeSlot.AppointmentEnd are defined ()BUT))
            // Schedule day workhours have no specified AppointmentLength ???
            // WE DON'T REALLY NEED AppointmentTimeSlot.AppointmentEnd either BECAUSE it's AppointmentTimeSlot.AppointmentStart + WorkHours.AppointmentLength

            // Appointment with timeslot
            if (this.ClientCanSelectTimeSlot && appointmentTimeSlot != null)
            {
                // Check if appointment is within working hours
                this.CheckBusinessRule(new AppointmentTimeSlotInWorkHours(this._workHours, appointmentTimeSlot));
                // Check if no overlapping schedules
                this.CheckBusinessRule(new AppointmentTimeSlotNotOverlapping(this._appointments, appointmentTimeSlot));

                Appointment newAppointment = 
                    Appointment.CreateAppointmentWithTimeSlot(this.CalendarDay ?? DateTime.UtcNow.Date, appointmentTimeSlot, contactInformation);

                this._appointments.Add(newAppointment);
            }
            // Appointment without timeslot
            else if (!this.ClientCanSelectTimeSlot && appointmentTimeSlot != null)
            {
                Appointment newAppointment =
                    Appointment.CreateAppointmentWithoutTimeSlot(this.CalendarDay ?? DateTime.UtcNow.Date, contactInformation);

                this._appointments.Add(newAppointment);
            }
            else 
            {
                throw new ArgumentException($"Placeholder exception. Cannot create appointment.");
            }
            
        }
    }
}
