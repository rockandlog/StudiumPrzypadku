namespace Zasobowo.API.Models.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string? AssignedTo { get; set; }
    }
}
