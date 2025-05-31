namespace Zasobowo.Client.Models
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
<<<<<<< HEAD
        public string? AssignedTo { get; set; }
=======
        public int? AssignedUserId { get; set; }
        public UserDto? AssignedUser { get; set; }
>>>>>>> develop
    }
}
