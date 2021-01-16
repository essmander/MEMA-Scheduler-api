using System;
using Dapper.Contrib.Extensions;

namespace MEMA_Planning_Schedule.Models
{
    public record Booking
    {
        // [Key]
        [ExplicitKey]
        public int BookingId { get; init; }
        public string ProjektName { get; init; }
        public string Customer { get; init; }
        public DateTime Start { get; init; }
        public DateTime Finish { get; init; }
    }
}