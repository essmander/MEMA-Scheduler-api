using System;
using Dapper.Contrib.Extensions;

namespace MEMA_Planning_Schedule.Models
{
    public record UserBooking
    {
        public int BookingId { get; init; }
        public string UserId { get; init; }
    }
}