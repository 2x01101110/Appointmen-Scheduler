using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.Idempotency
{
    public class CommandRequest
    {
        public string HashCode { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
