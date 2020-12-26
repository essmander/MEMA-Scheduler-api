using System;

namespace MEMA_Planning_Schedule.Models
{
    public record Booking
    {
        public int Id { get; init; }
        public string ProjectName { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
    }
}