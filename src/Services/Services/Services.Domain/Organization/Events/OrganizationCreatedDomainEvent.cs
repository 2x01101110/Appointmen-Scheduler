using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Organization.Events
{
    public class OrganizationCreatedDomainEvent : INotification
    {
        public Organization Organization { get; }

        public OrganizationCreatedDomainEvent(Organization organization)
        {
            this.Organization = organization;
        }
    }
}
