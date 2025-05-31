<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations.Schema;
=======
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> develop

namespace Zasobowo.API.Models
{
    public class Device
    {
        public int Id { get; set; }
<<<<<<< HEAD

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int? AssignedUserId { get; set; }

        public User? AssignedUser { get; set; }

        [NotMapped]
        public string AssignedTo => AssignedUser?.Username ?? "(nieprzypisany)";
=======
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public string? AssignedTo { get; set; }


        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
>>>>>>> develop
    }
}
