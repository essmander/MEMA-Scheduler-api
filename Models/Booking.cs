using System;
using Dapper.Contrib.Extensions;

namespace MEMA_Planning_Schedule.Models
{
    public record Booking
    {
        [ExplicitKey]
        public int Id { get; init; }
        public int WorkerId { get; init; }
        public string ProjectName { get; init; }
        public DateTime Start { get; init; }
        public DateTime Finish { get; init; }
    }
}