using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public class WorkHoursNotOverlapping : IBusinessRule
    {
        private readonly IReadOnlyCollection<WorkHours> _workHours;

        public WorkHoursNotOverlapping(List<WorkHours> workHours)
        {
            this._workHours = workHours;
        }

        public string Message => "Work hours are overlapping";

        public bool IsValid()
        {
            foreach (var workHours in this._workHours)
            {
                var filteredWorkHours = this._workHours.Where(x => x != workHours);

                if (filteredWorkHours.Count() > 0 && !filteredWorkHours.Any(x => x.WorkHoursStart >= workHours.WorkHoursStart && x.WorkHoursEnd >= workHours.WorkHoursEnd))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
