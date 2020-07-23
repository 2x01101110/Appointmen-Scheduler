using BuildingBlocks.Domain;
using Services.Domain.Organization.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Organization
{
    public class Organization : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }

        private Organization() { }

        private Organization(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;

            this.AddDomainEvent(new OrganizationCreatedDomainEvent(this));
        }

        public static Organization CreateOrganization(string name)
        {
            return new Organization(name);
        }
    }
}
