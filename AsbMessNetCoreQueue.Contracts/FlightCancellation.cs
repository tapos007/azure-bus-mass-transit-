using System;
using System.Collections.Generic;
using System.Text;

namespace AsbMessNetCoreQueue.Contracts
{
    public class FlightCancellation
    {
        public Guid FlightId { get; set; }

        public int CancellationId { get; set; }

        public string Message { get; set; } = "its queue example";
    }
}
