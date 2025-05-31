namespace Zasobowo.Client.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        public string Type { get; set; }
        public string Status { get; set; }
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; } // tylko do wyświetlania
=======
        public string Status { get; set; }
        public string? AssignedTo { get; set; }
        public string? Type { get; set; }
>>>>>>> develop
    }
}
