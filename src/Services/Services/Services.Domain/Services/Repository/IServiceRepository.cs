﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain.Services.Repository
{
    public interface IServiceRepository
    {
        Task<Service> GetServiceAsync(Guid id);
        void AddService(Service service);
        void UpdateService(Service service);
    }
}