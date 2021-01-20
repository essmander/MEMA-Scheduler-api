using System;
using Dapper.Contrib.Extensions;

namespace MEMA_Planning_Schedule.Models
{
    public record Booking
    {
        [Key]
        public int BookingId { get; init; }
        public string ProjectName { get; init; }
        public string Customer { get; init; }
        public DateTime Start { get; init; }
        public DateTime Finished { get; }
    }
}