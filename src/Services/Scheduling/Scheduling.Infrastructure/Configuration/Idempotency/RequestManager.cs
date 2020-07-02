using BuildingBlocks.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Configuration.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly SchedulingContext _context;

        public RequestManager(SchedulingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.FindAsync<ClientRequest>(id) != null;
        }
        
        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistsAsync(id);

            if (exists)
            {
                throw new Exception($"Request with {id} already exists");
            }
            else 
            {
                _context.Add(new ClientRequest
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
