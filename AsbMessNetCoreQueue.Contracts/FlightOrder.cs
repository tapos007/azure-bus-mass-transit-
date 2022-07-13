using System;
using System.Collections.Generic;
using System.Text;

namespace AsbMessNetCoreQueue.Contracts
{
    public class FlightOrder
    {
        public Guid FlightId { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; } = "its topic example";
    }
}
