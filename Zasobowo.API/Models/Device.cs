using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zasobowo.API.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Status { get; set; }

        public int? AssignedUserId { get; set; }

        [ForeignKey("AssignedUserId")]
        public User? AssignedUser { get; set; }
    }
}
