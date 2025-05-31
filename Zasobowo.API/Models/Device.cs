using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zasobowo.API.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public string? AssignedTo { get; set; }


        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}
