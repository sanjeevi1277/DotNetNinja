﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class User
    {
        [Key]

        public string EmployeeID { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string EmailVerificationToken { get; set; }

        public DateTime TokenGeneratedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
