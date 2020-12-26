namespace MEMA_Planning_Schedule.Models
{
    public record User
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Mobile { get; init; }
        public string SocialNumber { get; init; }
    }
}