using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Application.Commands.CreateService
{
    //[DataContract]
    public class CreateServiceCommand : IRequest
    {
        //[DataMember]
        public Guid OrganizationId { get; set; }

        //[DataMember]
        public string Name { get; set; }

        //[DataMember]
        public bool CanSelectStaff { get; set; }

        //[DataMember]
        public bool Available { get; set; }

        ////public CreateServiceCommand(Guid organizationId, string name, bool canSelectStaff, bool available)
        ////{
        ////    this.OrganizationId = organizationId;
        ////    this.Name = name;
        ////    this.CanSelectStaff = canSelectStaff;
        ////    this.Available = available;
        ////}
    }
}
