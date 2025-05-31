namespace Zasobowo.Client.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string? AssignedTo { get; set; }
        public string? Type { get; set; }
    }
}
